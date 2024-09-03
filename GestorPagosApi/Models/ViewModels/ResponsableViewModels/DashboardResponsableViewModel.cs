using GestorPagosApi.DTOs;

namespace GestorPagosApi.Models.ViewModels.ResponsableViewModels
{
    public class DashboardResponsableViewModel
    {
        public decimal costoTotalTemporada { get; set; }
        public decimal saldoPagado { get; set; }
        public decimal saldoPendiente { get; set; }
        public decimal cantidadAbonar { get; set; }
        public List<HistorialPagos>? listaPagos { get; set; }
    }

    public class HistorialPagos : PagoDTO
    {
        //public string jugadorNavigation { get; set; } = string.Empty;
        public string descripcion { get; set; } = "Pago de factura";
    }
}
