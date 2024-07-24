using SIGET.DataAccess.Repository.IRepository;
using SIGET.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IColaboradoresRepository Colaboradores { get; }
        void Save();
    }
}