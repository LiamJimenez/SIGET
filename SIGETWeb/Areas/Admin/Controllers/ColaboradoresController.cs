using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Practica.DataAccess.Repository.IRepository;
using SIGET.DataAccess.Data;
using SIGET.DataAccess.Repository.IRepository;
using SIGET.Models;
using SIGET.Models.ViewModel;


namespace SIGETWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColaboradoresController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ColaboradoresController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Colaboradores> objColaboradoresList = _unitOfWork.Colaboradores.GetAll().ToList();
            return View(objColaboradoresList);
        }

        public IActionResult Upsert(int? id)
        {

            ColaboradorVM colaboradorVM = new()
            {
                ColaboradoresList = _unitOfWork.Colaboradores.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                }),
                Colaboradores = new Colaboradores()
            };
            if (id == null || id == 0)
            {
                return View(colaboradorVM);
            }
            else
            {
                colaboradorVM.Colaboradores = _unitOfWork.Colaboradores.Get(u => u.Id == id);
                return View(colaboradorVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ColaboradorVM colaboradorVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string colaboradorPath = Path.Combine(wwwRootPath, @"images\colaborador");

                    if (!string.IsNullOrEmpty(colaboradorVM.Colaboradores.ImageUrl))
                    {
                        var oldImagePath =
                            Path.Combine(wwwRootPath, colaboradorVM.Colaboradores.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(colaboradorPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    colaboradorVM.Colaboradores.ImageUrl = @"\images\colaborador\" + fileName;
                }

                if (colaboradorVM.Colaboradores.Id == 0)
                {
                    _unitOfWork.Colaboradores.Add(colaboradorVM.Colaboradores);
                }
                else
                {
                    _unitOfWork.Colaboradores.Update(colaboradorVM.Colaboradores);
                }
                _unitOfWork.Save();
                TempData["exito"] = "Colaborador agregado correctamente";
                return RedirectToAction("Index");
            }
            else
            {
                colaboradorVM.ColaboradoresList = _unitOfWork.Colaboradores.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                });
                return View(colaboradorVM);
            }
        }

        public IActionResult Detalles(int id)
        {
            var colaborador = _unitOfWork.Colaboradores.Get(c => c.Id == id);
            if (colaborador == null)
            {
                return NotFound();
            }
            return View(colaborador);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Colaboradores> objColaboradoresList = _unitOfWork.Colaboradores.GetAll(includeProperties: "Colaborador").ToList();
            return Json(new { data = objColaboradoresList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var colaboradorToBeDeleted = _unitOfWork.Colaboradores.Get(u => u.Id == id);
            if (colaboradorToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error al eliminar al colaborador" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, colaboradorToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Colaboradores.Remove(colaboradorToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Eliminado exitosamente" });
        }

        #endregion 

    }
}