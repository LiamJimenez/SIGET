using SIGET.DataAccess.Repository;
using SIGET.DataAccess.Repository.IRepository;
using SIGET.DataAccess.Data;
using SIGET.Repository;

namespace SIGET.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {      
        private ApplicationDbContext _db;
        public IColaboradoresRepository Colaboradores { get; private set; }
        public IComponentesFisicosRepository ComponentesFisicos { get; private set; }
        public ILicenciasRepository Licencias { get; private set; }
        public IServiciosRepository Servicios { get; private set; }
        public IPedidosRepository Pedidos { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {      
            _db = db;
            Colaboradores = new ColaboradoresRepository(_db);
            ComponentesFisicos = new ComponentesFisicosRepository(_db);
            Licencias = new LicenciasRepository(_db);
            Servicios = new ServiciosRepository(_db);
            Pedidos = new PedidosRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}