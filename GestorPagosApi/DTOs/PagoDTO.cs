
namespace GestorPagosApi.DTOs
{
    public class PagoDTO
    {
        public int idPago { get; set; }

        public DateTime fechaPago { get; set; }

        public int idJugador { get; set; }

        public decimal cantidadPago { get; set; }

        public int? idResponsable { get; set; }

        //public virtual JugadorDTO IdJugadorNavigation { get; set; } = null!;

        //public virtual UsuarioDTO? IdResponsableNavigation { get; set; }
    }
}
