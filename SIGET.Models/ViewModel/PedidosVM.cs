using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGET.Models.ViewModel
{
    public class PedidosVM
    {
        public Pedidos Pedidos { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ColaboradoresList { get; set; }

        public IEnumerable<SelectListItem> ServiciosList { get; set; }
    }
}
