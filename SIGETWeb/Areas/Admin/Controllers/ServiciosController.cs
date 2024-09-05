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
    public class ServiciosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ServiciosController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Servicios> objServiciosList = _unitOfWork.Servicios.GetAll(includeProperties: "ComponentesFisicos, Licencias").ToList();
            return View(objServiciosList);
        }
        
        public IActionResult Upsert(int? id)
        {
            ServiciosVM serviciosVM = new()
            {
                ComponentesFisicosList = _unitOfWork.ComponentesFisicos.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                }),
                LicenciasList = _unitOfWork.Licencias.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                }),
                Servicios = new Servicios()
            };
            if (id == null || id == 0)
            {
                return View(serviciosVM);
            }
            else
            {
                serviciosVM.Servicios = _unitOfWork.Servicios.Get(u => u.Id == id);
                return View(serviciosVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ServiciosVM serviciosVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string serviciosPath = Path.Combine(wwwRootPath, @"images\servicio");

                    if (!string.IsNullOrEmpty(serviciosVM.Servicios.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, serviciosVM.Servicios.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(serviciosPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    serviciosVM.Servicios.ImageUrl = @"\images\servicio\" + fileName;
                }

                if (serviciosVM.Servicios.ComponentesFisicosId.HasValue && serviciosVM.Servicios.LicenciasId.HasValue)
                {
                    ModelState.AddModelError("", "Solo puede seleccionar un Componente Físico o una Licencia, no ambos");
                }
                else if (!serviciosVM.Servicios.ComponentesFisicosId.HasValue && !serviciosVM.Servicios.LicenciasId.HasValue)
                {
                    ModelState.AddModelError("", "Debe seleccionar un Componente Físico o una Licencia");
                }
                else
                {
                    if (serviciosVM.Servicios.ComponentesFisicosId.HasValue)
                    {
                        serviciosVM.Servicios.LicenciasId = null;
                    }
                    else if (serviciosVM.Servicios.LicenciasId.HasValue)
                    {
                        serviciosVM.Servicios.ComponentesFisicosId = null;
                    }

                    if (serviciosVM.Servicios.Id == 0)
                    {
                        _unitOfWork.Servicios.Add(serviciosVM.Servicios);
                        TempData["exito"] = "Servicio creado correctamente";
                    }
                    else
                    {
                        _unitOfWork.Servicios.Update(serviciosVM.Servicios);
                        TempData["exito"] = "Servicio actualizado correctamente";
                    }
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }

            serviciosVM.ComponentesFisicosList = _unitOfWork.ComponentesFisicos.GetAll().Select(u => new SelectListItem
            {
                Text = u.Nombre,
                Value = u.Id.ToString()
            });
            serviciosVM.LicenciasList = _unitOfWork.Licencias.GetAll().Select(u => new SelectListItem
            {
                Text = u.Nombre,
                Value = u.Id.ToString()
            });

            return View(serviciosVM);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Servicios> objServiciosList = _unitOfWork.Servicios.GetAll(includeProperties: "ComponentesFisicos, Licencias").ToList();
            return Json(new { data = objServiciosList });
        }

        public IActionResult Detalles(int id)
        {
            var servicios = _unitOfWork.Servicios.Get(c => c.Id == id);
            if (servicios == null)
            {
                return NotFound();
            }
            return View(servicios);
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var serviciosToBeDeleted = _unitOfWork.Servicios.Get(u => u.Id == id);
            if (serviciosToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error eliminando servicio" });
            }

            var oldImagePath =
                            Path.Combine(_webHostEnvironment.WebRootPath,
                            serviciosToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Servicios.Remove(serviciosToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Eliminado correctamente" });
        }

        #endregion
    }
}