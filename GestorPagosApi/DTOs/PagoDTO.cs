
namespace GestorPagosApi.DTOs
{
    public class PagoDTO
    {
        public int IdPago { get; set; }

        public DateTime FechaPago { get; set; }

        public int IdJugador { get; set; }

        public decimal CantidadPago { get; set; }

        //public JugadorDTO? IdJugadorNavigation { get; set; } 
    }
}
