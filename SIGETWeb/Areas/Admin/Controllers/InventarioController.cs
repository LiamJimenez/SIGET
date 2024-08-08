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
    public class InventarioController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public InventarioController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Inventario> objInventariolist = _unitOfWork.Inventario.GetAll().ToList();
            return View(objInventariolist);
        }

        public IActionResult Upsert(int? id)
        {

            InventarioVM inventarioVM = new()
            {
                InventarioList = _unitOfWork.Inventario.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                }),
                Inventario = new Inventario()
            };
            if (id == null || id == 0)
            {
                return View(inventarioVM);
            }
            else
            {
                inventarioVM.Inventario = _unitOfWork.Inventario.Get(u => u.Id == id);
                return View(inventarioVM);
            }

        }

        [HttpPost]
        public IActionResult Upsert(InventarioVM inventarioVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string inventarioPath = Path.Combine(wwwRootPath, @"images\inventario");

                    if (!string.IsNullOrEmpty(inventarioVM.Inventario.ImageUrl))
                    {
                        var oldImagePath =
                            Path.Combine(wwwRootPath, inventarioVM.Inventario.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(inventarioPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    inventarioVM.Inventario.ImageUrl = @"\images\inventario\" + fileName;
                }

                if (inventarioVM.Inventario.Id == 0)
                {
                    _unitOfWork.Inventario.Add(inventarioVM.Inventario);
                }
                else
                {
                    _unitOfWork.Inventario.Update(inventarioVM.Inventario);
                }
                _unitOfWork.Save();
                TempData["exito"] = "Nuevo producto agregado correctamente";
                return RedirectToAction("Index");
            }
            else
            {
                inventarioVM.InventarioList = _unitOfWork.Inventario.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                });
                return View(inventarioVM);
            }
        }
    }
}