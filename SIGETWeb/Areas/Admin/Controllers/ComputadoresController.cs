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
    public class ComputadoresController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ComputadoresController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Computadores> objComputadoresList = _unitOfWork.Computadores.GetAll().ToList();
            return View(objComputadoresList);
        }

        public IActionResult Upsert(int? id)
        {

            ComputadoresVM computadoresVM = new()
            {
                ComputadoresList = _unitOfWork.Computadores.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                }),
                Computadores = new Computadores()
            };
            if (id == null || id == 0)
            {
                return View(computadoresVM);
            }
            else
            {
                computadoresVM.Computadores = _unitOfWork.Computadores.Get(u => u.Id == id);
                return View(computadoresVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ComputadoresVM computadoresVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {

                if (computadoresVM.Computadores.Id == 0)
                {
                    _unitOfWork.Computadores.Add(computadoresVM.Computadores);
                }
                else
                {
                    _unitOfWork.Computadores.Update(computadoresVM.Computadores);
                }
                _unitOfWork.Save();
                TempData["exito"] = "Computador agregado correctamente";
                return RedirectToAction("Index");
            }
            else
            {
                computadoresVM.ComputadoresList = _unitOfWork.Computadores.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                });
                return View(computadoresVM);
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Computadores> objComputadoresList = _unitOfWork.Computadores.GetAll(includeProperties: "Computadores").ToList();
            return Json(new { data = objComputadoresList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var computadorToBeDeleted = _unitOfWork.Computadores.Get(u => u.Id == id);
            if (computadorToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error al eliminar al computador" });
            }

            _unitOfWork.Computadores.Remove(computadorToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Eliminado exitosamente" });
        }


        #endregion 
    }
}