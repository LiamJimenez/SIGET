using SIGET.DataAccess.Data;
using SIGET.DataAccess.Repository.IRepository;
using SIGET.Models;
using SIGET.Repository;
using SIGET.Models;
using Practica.DataAccess.Repository.IRepository;


namespace SIGET.Repository
{
    public class ComponentesFisicosRepository : Repository<ComponentesFisicos>, IComponentesFisicosRepository
    {
        private ApplicationDbContext _db;
        public ComponentesFisicosRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ComponentesFisicos obj)
        {
            _db.componentesfisicos.Update(obj);
        }
    }
}