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
                TempData["success"] = "Product created successfully";
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

        public IActionResult Detalles(int colaboradorId)
        {
            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Colaboradores? colaboradoresFromDb = _unitOfWork.Colaboradores.Get(u => u.Id == id);
            if (colaboradoresFromDb == null)
            {
                return NotFound();
            }
            return View(colaboradoresFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Colaboradores? obj = _unitOfWork.Colaboradores.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Colaboradores.Remove(obj);
            _unitOfWork.Save();
            TempData["exito"] = "Colaborador eliminado correctamente";
            return RedirectToAction("Index");
        }
    }
}
