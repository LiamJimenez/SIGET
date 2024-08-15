using SIGET.DataAccess.Data;
using SIGET.DataAccess.Repository.IRepository;
using SIGET.Models;
using SIGET.Repository;


namespace SIGET.Repository
{
    public class LicenciasRepository : Repository<Licencias>, ILicenciasRepository
    {
        private ApplicationDbContext _db;
        public LicenciasRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Licencias obj)
        {
            _db.licencias.Update(obj);
        }
    }
}