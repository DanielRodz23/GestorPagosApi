
namespace GestorPagosApi.DTOs
{
    public class PagoDTO
    {
        public int IdPago { get; set; }

        public DateTime FechaPago { get; set; }

        public int IdJugador { get; set; }

        public decimal CantidadPago { get; set; }

        public int? IdResponsable { get; set; }

        //public virtual JugadorDTO IdJugadorNavigation { get; set; } = null!;

        //public virtual UsuarioDTO? IdResponsableNavigation { get; set; }
    }
}
