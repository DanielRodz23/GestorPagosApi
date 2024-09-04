using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorPagosApi.Models.ViewModels.TesoreroViewModels
{
    public class TesoreroIndexViewModel
    {
        public int IdSeleccionado { get; set; }
        public List<Personas> ListaPersonas { get; set; } = [];
        public decimal Pago { get; set; }
        public decimal SaldoPendiente { get; set; }
    }
    public class Personas
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}