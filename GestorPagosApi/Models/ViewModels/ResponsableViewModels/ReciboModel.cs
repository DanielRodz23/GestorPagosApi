using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorPagosApi.Models.ViewModels.ResponsableViewModels
{
    public class ReciboModel
    {
        public int NumRecibo { get; set; }
        public decimal CantidadPago { get; set; }
        public string Descripcion { get; set; } = "Pago de temporada";
        public string NombreResponsable { get; set; } = null!;
        public string RecibidoPor { get; set; } = "Tesorero";
        public string FormaPago { get; set; }="Efectivo";
        public DateTime Fecha { get; set; }
    }
}