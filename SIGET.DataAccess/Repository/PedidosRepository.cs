using SIGET.DataAccess.Data;
using SIGET.DataAccess.Repository.IRepository;
using SIGET.Models;

namespace SIGET.Repository
{
    public class PedidosRepository : Repository<Pedidos>, IPedidosRepository
    {
        private ApplicationDbContext _db;
        public PedidosRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Pedidos obj)
        {
            var objFromDb = _db.pedidos.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.ColaboradoresId = obj.ColaboradoresId;
                objFromDb.ServiciosId = obj.ServiciosId;
                objFromDb.Cantidad = obj.Cantidad;
            }
        }
    }
}