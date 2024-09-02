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
    public class PedidosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PedidosController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Pedidos> objPedidosList = _unitOfWork.Pedidos.GetAll(includeProperties: "Colaboradores, Servicios").ToList();
            return View(objPedidosList);
        }

        public IActionResult Upsert(int? id)
        {
            PedidosVM pedidosVM = new()
            {
                ColaboradoresList = _unitOfWork.Colaboradores.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                }),
                ServiciosList = _unitOfWork.Servicios.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                }),
                Pedidos = new Pedidos()
            };
            if (id == null || id == 0)
            {
                return View(pedidosVM);
            }
            else
            {
                pedidosVM.Pedidos = _unitOfWork.Pedidos.Get(u => u.Id == id);
                return View(pedidosVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(PedidosVM pedidosVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (pedidosVM.Pedidos.ColaboradoresId == 0)
                {
                    pedidosVM.Pedidos.ColaboradoresId = null;
                }
                if (pedidosVM.Pedidos.ServiciosId == 0)
                {
                    pedidosVM.Pedidos.ServiciosId = null;
                }

                if (pedidosVM.Pedidos.Id == 0)
                {
                    _unitOfWork.Pedidos.Add(pedidosVM.Pedidos);
                }
                else
                {
                    _unitOfWork.Pedidos.Update(pedidosVM.Pedidos);
                }
                _unitOfWork.Save();
                TempData["exito"] = "Servicio agregado correctamente";
                return RedirectToAction("Index");
            }
            else
            {
                pedidosVM.ColaboradoresList = _unitOfWork.Colaboradores.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                });
                pedidosVM.ServiciosList = _unitOfWork.Servicios.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                });
                return View(pedidosVM);
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Pedidos> objPedidosList = _unitOfWork.Pedidos.GetAll(includeProperties: "Colaboradores, Servicios").ToList();
            return Json(new { data = objPedidosList });
        }

        public IActionResult Detalles(int id)
        {
            var pedidos = _unitOfWork.Pedidos.Get(c => c.Id == id);
            if (pedidos == null)
            {
                return NotFound();
            }
            return View(pedidos);
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var pedidosToBeDeleted = _unitOfWork.Pedidos.Get(u => u.Id == id);
            if (pedidosToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error eliminando servicio" });
            }

            _unitOfWork.Pedidos.Remove(pedidosToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Eliminado correctamente" });
        }

        #endregion
    }
}