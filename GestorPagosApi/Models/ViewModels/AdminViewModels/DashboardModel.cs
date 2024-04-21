using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorPagosApi.DTOs;

namespace GestorPagosApi.Models.ViewModels.AdminViewModels
{
    public class DashboardModel
    {
        public int Totaljugadores { get; set; }
        public int TotalTemporadas { get; set; }
        public int TotalCategorias { get; set; }
        public int TotalPagos { get; set; }
        public List<DashPago>? ListaPagos { get; set; }
        public List<DashJugador>? ListaJugadores { get; set; }
    }
    public class DashPago : PagoDTO
    {
        public string JugadorNavigation { get; set; } = string.Empty;
        public string ResponsableNavigation { get; set; } = string.Empty;

    }
    public class DashJugador : JugadorDTO
    {
        public string CategoriaNavigation { get; set; } = string.Empty;
    }
}