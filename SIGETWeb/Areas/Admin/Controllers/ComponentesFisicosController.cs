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
    public class ComponentesFisicosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ComponentesFisicosController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<ComponentesFisicos> objComponentesFisicosList = _unitOfWork.ComponentesFisicos.GetAll().ToList();
            return View(objComponentesFisicosList);
        }

        public IActionResult Upsert(int? id)
        {
            ComponentesFisicosVM componentesFisicosVM = new()
            {
                ComponentesFisicosList = _unitOfWork.ComponentesFisicos.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                }),
                ComponentesFisicos = new ComponentesFisicos()
            };
            if (id == null || id == 0)
            {
                return View(componentesFisicosVM);
            }
            else
            {
                componentesFisicosVM.ComponentesFisicos = _unitOfWork.ComponentesFisicos.Get(u => u.Id == id);
                return View(componentesFisicosVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ComponentesFisicosVM componentesFisicosVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string componentesFisicosPath = Path.Combine(wwwRootPath, @"images\componentesfisicos");

                    if (!string.IsNullOrEmpty(componentesFisicosVM.ComponentesFisicos.ImageUrl))
                    {
                        var oldImagePath =
                            Path.Combine(wwwRootPath, componentesFisicosVM.ComponentesFisicos.ImageUrl.TrimStart('\\'));
                        
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    
                    using (var fileStream = new FileStream(Path.Combine(componentesFisicosPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    
                    componentesFisicosVM.ComponentesFisicos.ImageUrl = @"\images\componentesfisicos\" + fileName;
                } 

                if (componentesFisicosVM.ComponentesFisicos.Id == 0)
                {
                    _unitOfWork.ComponentesFisicos.Add(componentesFisicosVM.ComponentesFisicos);
                }
                else
                {
                    _unitOfWork.ComponentesFisicos.Update(componentesFisicosVM.ComponentesFisicos);
                }
                _unitOfWork.Save();
                TempData["exito"] = "Componente agregado correctamente";
                return RedirectToAction("Index");
            }
            else
            {
                componentesFisicosVM.ComponentesFisicosList = _unitOfWork.ComponentesFisicos.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                });
                return View(componentesFisicosVM);
            }
        }

        public IActionResult Detalles(int id)
        {
            var componentesfisicos = _unitOfWork.ComponentesFisicos.Get(c => c.Id == id);
            if (componentesfisicos == null)
            {
                return NotFound();
            }
            return View(componentesfisicos);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ComponentesFisicos> ComponentesfisicosList = _unitOfWork.ComponentesFisicos.GetAll(includeProperties: "ComponentesFisicos").ToList();
            return Json(new { data = ComponentesfisicosList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var componentefisicoToBeDeleted = _unitOfWork.ComponentesFisicos.Get(u => u.Id == id);
            if (componentefisicoToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error al eliminar al componente" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, componentefisicoToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.ComponentesFisicos.Remove(componentefisicoToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Eliminado exitosamente" });
        }

        #endregion 
    }
}