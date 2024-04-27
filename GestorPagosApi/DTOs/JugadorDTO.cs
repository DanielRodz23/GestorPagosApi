
namespace GestorPagosApi.DTOs
{
    public class JugadorDTO
    {
        public int idJugador { get; set; }

        public string nombre { get; set; } = null!;

        public DateOnly dob { get; set; }

        public int idUsuario { get; set; }

        public int idTemporada { get; set; }

        public decimal deuda { get; set; }
        public bool exists { get; set; }
        public int? idCategoria { get; set; }

        //public TemporadaDTO? IdTemporadaNavigation { get; set; } 

        //public ICollection<PagoDTO>? Pago { get; set; } 
    }
}
