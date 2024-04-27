
namespace GestorPagosApi.DTOs
{
    public class TemporadaDTO
    {
        public int idTemporada { get; set; }
        public string nombre { get; set; } = null!;
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinal { get; set; }
        public int? idCategoria { get; set; }
        public decimal costo { get; set; }
        public bool? tempActual { get; set; }
        public bool? exists { get; set; }
        public CategoriaDTO? idCategoriaNavigation { get; set; }
        public ICollection<JugadorDTO>? jugador { get; set; }
    }
}
