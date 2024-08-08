using SIGET.DataAccess.Repository.IRepository;
using SIGET.Models;
using SIGET.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica.DataAccess.Repository.IRepository
{
    public interface IInventarioRepository: IRepository<Inventario>
    {
        void Update(Inventario obj);
    }
}