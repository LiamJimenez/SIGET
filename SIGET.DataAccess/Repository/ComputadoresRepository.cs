using SIGET.DataAccess.Data;
using SIGET.DataAccess.Repository.IRepository;
using SIGET.Models;
using SIGET.Repository;


namespace SIGET.Repository
{
    public class ComputadoresRepository : Repository<Computadores>, IComputadoresRepository
    {
        private ApplicationDbContext _db;
        public ComputadoresRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Computadores obj)
        {
            _db.computadores.Update(obj);
        }
    }
}