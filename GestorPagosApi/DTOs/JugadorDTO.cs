
namespace GestorPagosApi.DTOs
{
    public class JugadorDTO
    {
        public int IdJugador { get; set; }

        public string Nombre { get; set; } = null!;

        public DateOnly Dob { get; set; }

        public int IdUsuario { get; set; }

        public int IdTemporada { get; set; }

        public decimal Deuda { get; set; }
        public bool Exists { get; set; }

        //public TemporadaDTO? IdTemporadaNavigation { get; set; } 

        //public ICollection<PagoDTO>? Pago { get; set; } 
    }
}
