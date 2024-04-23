
namespace GestorPagosApi.DTOs
{
    public class CategoriaDTO
    {
        public int idCategoria { get; set; }

        public string nombreCategoria { get; set; } = null!;
        public int? edadInicial { get; set; }

        public int? edadTermino { get; set; }

        public ICollection<TemporadaDTO>? temporada { get; set; } 
    }
}
