using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGET.Models.ViewModel
{
    public class ServiciosVM
    {
        public Servicios Servicios { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ServiciosList { get; set; }
    }
}
