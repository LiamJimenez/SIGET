using SIGET.DataAccess.Data;
using SIGET.DataAccess.Repository.IRepository;
using SIGET.Models;
using SIGET.Repository;


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
            var objFromDb = _db.componentesfisicos.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Nombre = obj.Nombre;
                objFromDb.Cantidad = obj.Cantidad;
                objFromDb.PuntoReabastecimiento = obj.PuntoReabastecimiento;
                objFromDb.Descripcion = obj.Descripcion;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}