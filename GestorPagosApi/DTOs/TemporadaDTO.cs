
namespace GestorPagosApi.DTOs
{
    public class TemporadaDTO
    {
        public int IdTemporada { get; set; }

        public string Nombre { get; set; } = null!;

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFinal { get; set; }

        public int? IdCategoria { get; set; }

        public decimal Costo { get; set; }

        public CategoriaDTO? IdCategoriaNavigation { get; set; }

        public ICollection<JugadorDTO> Jugador { get; set; }
    }
}
