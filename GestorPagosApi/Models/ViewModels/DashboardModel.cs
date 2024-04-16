using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorPagosApi.DTOs;

namespace GestorPagosApi.Models.ViewModels
{
    public class DashboardModel
    {
        public int Totaljugadores { get; set; }
        public int TotalTemporadas { get; set; }
        public int TotalCategorias { get; set; }
        public int TotalPagos { get; set; }
        public List<PagoDTO> ListaPagos { get; set; }
        public List<JugadorDTO> ListaJugadores { get; set; }
    }
}