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
            var objFromDb = _db.servicios.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Nombre = obj.Nombre;
                objFromDb.ComponentesFisicosId = obj.ComponentesFisicosId;
                objFromDb.LicenciasId = obj.LicenciasId;
                objFromDb.Descripcion = obj.Descripcion;
                objFromDb.Precio = obj.Precio;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}