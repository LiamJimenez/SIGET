using SIGET.DataAccess.Repository.IRepository;
using SIGET.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGET.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IColaboradoresRepository Colaboradores { get; }
        IComponentesFisicosRepository ComponentesFisicos { get; }
        ILicenciasRepository Licencias { get; }
        void Save();
    }
}