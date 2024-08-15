using SIGET.Models;
using SIGET.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGET.DataAccess.Repository.IRepository
{
    public interface ILicenciasRepository : IRepository<Licencias>
    {
        void Update(Licencias obj);
    }
}
