using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGET.Models.ViewModel
{
    public class LicenciasVM
    {
        public Licencias Licencias { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> LicenciasList { get; set; }
    }
}
