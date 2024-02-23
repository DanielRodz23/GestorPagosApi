
namespace GestorPagosApi.DTOs
{
    public class CategoriaDTO
    {
        public int IdCategoria { get; set; }

        public string NombreCategoria { get; set; } = null!;

        public ICollection<TemporadaDTO> Temporada { get; set; } 
    }
}
