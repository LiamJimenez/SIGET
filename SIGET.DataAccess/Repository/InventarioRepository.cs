using SIGET.DataAccess.Data;
using SIGET.DataAccess.Repository.IRepository;
using SIGET.Models;
using SIGET.Repository;
using SIGET.Models;
using Practica.DataAccess.Repository.IRepository;


namespace SIGET.Repository
{
    public class InventarioRepository : Repository<Inventario>, IInventarioRepository
    {
        private ApplicationDbContext _db;
        public InventarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Inventario obj)
        {
            _db.inventario.Update(obj);
        }
    }
}