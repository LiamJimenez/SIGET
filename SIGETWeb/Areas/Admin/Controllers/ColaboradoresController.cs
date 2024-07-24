using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Practica.DataAccess.Repository.IRepository;
using SIGET.DataAccess.Data;
using SIGET.DataAccess.Repository.IRepository;
using SIGET.Models;


namespace SIGETWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColaboradoresController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ColaboradoresController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Colaboradores> objColaboradoresList = _unitOfWork.Colaboradores.GetAll().ToList();
            return View(objColaboradoresList);
        }

        public IActionResult Detalles(int colaboradorId)
        {
            return View();

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Colaboradores obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Colaboradores.Add(obj);
                _unitOfWork.Save();
                TempData["exito"] = "Colaborador creado correctamente";

                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Edit(int? id)
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
        [HttpPost]
        public IActionResult Edit(Colaboradores obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Colaboradores.Update(obj);
                _unitOfWork.Save();
                TempData["exito"] = "Colaborador editado correctamente";
                return RedirectToAction("Index");
            }
            return View(obj);

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
