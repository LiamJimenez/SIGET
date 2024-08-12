using Practica.DataAccess.Repository;
using Practica.DataAccess.Repository.IRepository;
using SIGET.DataAccess.Data;
using SIGET.DataAccess.Data;
using SIGET.DataAccess.Repository.IRepository;
using SIGET.Repository;

namespace SIGET.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IColaboradoresRepository Colaboradores { get; private set; }
        public IComponentesFisicosRepository ComponentesFisicos { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Colaboradores = new ColaboradoresRepository(_db);
            ComponentesFisicos = new ComponentesFisicosRepository(_db);

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}