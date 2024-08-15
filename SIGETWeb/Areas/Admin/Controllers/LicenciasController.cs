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
    public class LicenciasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LicenciasController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Licencias> objLicenciasList = _unitOfWork.Licencias.GetAll().ToList();
            return View(objLicenciasList);
        }

        public IActionResult Upsert(int? id)
        {
            LicenciasVM licenciasVM = new()
            {
                LicenciasList = _unitOfWork.Licencias.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                }),
                Licencias = new Licencias()
            };
            if (id == null || id == 0)
            {
                return View(licenciasVM);
            }
            else
            {
                licenciasVM.Licencias = _unitOfWork.Licencias.Get(u => u.Id == id);
                return View(licenciasVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(LicenciasVM licenciasVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string licenciaPath = Path.Combine(wwwRootPath, @"images\licencia");

                    if (!string.IsNullOrEmpty(licenciasVM.Licencias.ImageUrl))
                    {
                        var oldImagePath =
                            Path.Combine(wwwRootPath, licenciasVM.Licencias.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(licenciaPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    licenciasVM.Licencias.ImageUrl = @"\images\licencia\" + fileName;
                }

                if (licenciasVM.Licencias.Id == 0)
                {
                    _unitOfWork.Licencias.Add(licenciasVM.Licencias);
                }
                else
                {
                    _unitOfWork.Licencias.Update(licenciasVM.Licencias);
                }
                _unitOfWork.Save();
                TempData["exito"] = "Licencia agregada correctamente";
                return RedirectToAction("Index");
            }
            else
            {
                licenciasVM.LicenciasList = _unitOfWork.Licencias.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                });
                return View(licenciasVM);
            }
        }

        public IActionResult Detalles(int id)
        {
            var licencias = _unitOfWork.Licencias.Get(c => c.Id == id);
            if (licencias == null)
            {
                return NotFound();
            }
            return View(licencias);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Licencias> objLicenciasList = _unitOfWork.Licencias.GetAll(includeProperties: "Licencia").ToList();
            return Json(new { data = objLicenciasList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var licenciaToBeDeleted = _unitOfWork.Licencias.Get(u => u.Id == id);
            if (licenciaToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error al eliminar la licencia" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, licenciaToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Licencias.Remove(licenciaToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Eliminado exitosamente" });
        }

        #endregion 

    }
}