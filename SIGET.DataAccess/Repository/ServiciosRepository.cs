using SIGET.DataAccess.Data;
using SIGET.DataAccess.Repository.IRepository;
using SIGET.Models;

namespace SIGET.Repository
{
    public class ServiciosRepository : Repository<Servicios>, IServiciosRepository
    {
        private ApplicationDbContext _db;
        public ServiciosRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Servicios obj)
        {
            _db.servicios.Update(obj);
        }
    }
}