using SIGET.DataAccess.Data;
using SIGET.DataAccess.Repository.IRepository;
using SIGET.Models;
using SIGET.Repository;
using SIGET.Models;


namespace SIGET.Repository
{
    public class ColaboradoresRepository : Repository<Colaboradores>, IColaboradoresRepository
    {
        private ApplicationDbContext _db;
        public ColaboradoresRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Colaboradores obj)
        {
            _db.colaboradores.Update(obj);
        }
    }
}