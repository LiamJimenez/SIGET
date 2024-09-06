using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using SIGET.DataAccess.Repository.IRepository;
using SIGET.DataAccess.Data;
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
            List<Colaboradores> objColaboradoresList = _unitOfWork.Colaboradores.GetAll(includeProperties: "Computadores").ToList();

            List<ColaboradorComputador>? colaboradorComputadores = new();

            if (objColaboradoresList != null)
            {

                foreach (var colaboradores in objColaboradoresList)
                {
                    Computadores objComputador = _unitOfWork.Computadores.Get(a => a.Id == colaboradores.ComputadoresId);

                    colaboradorComputadores.Add(new ColaboradorComputador
                    {
                        Colaboradores = colaboradores,
                        Computadores = objComputador
                    });

                }
            }
            return View(colaboradorComputadores);
        }

        public IActionResult Upsert(int? id)
        {
        
            ColaboradorVM colaboradorVM = new()
            {

                ComputadoresList = _unitOfWork.Computadores.GetAllSet(filter: a=>a.Estado).Select(u => new SelectListItem
                {
                    Text = u.Ip,
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

                var computador = _unitOfWork.Computadores.Get(a=>a.Id.Equals(colaboradorVM.Colaboradores.ComputadoresId));

                if (computador is not null)
                {
                    computador.Estado = false;
                    _unitOfWork.Computadores.Update(computador);
                }
                _unitOfWork.Save();
                TempData["exito"] = "Colaborador agregado correctamente";
                return RedirectToAction("Index");
            }
            else
            {
                colaboradorVM.ComputadoresList = _unitOfWork.Computadores.GetAll().Select(u => new SelectListItem
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

            var computador = _unitOfWork.Computadores.Get(filter: a => a.Id.Equals(colaboradorToBeDeleted.ComputadoresId));

            if (computador.Estado is false && computador is not null)
            {
                computador.Estado = true;
                _unitOfWork.Computadores.Update(computador);
            }

            var pedidos = _unitOfWork.Pedidos.GetAllSet(filter: a=>a.ColaboradoresId == colaboradorToBeDeleted.Id);

            if (pedidos is not null)
            {
                foreach (var pedidosAll in pedidos)
                {
                    _unitOfWork.Pedidos.Remove(pedidosAll);
                }
            }

            _unitOfWork.Colaboradores.Remove(colaboradorToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Eliminado exitosamente" });
        }

        #endregion 

    }
}