
namespace GestorPagosApi.Models.Entities;

public partial class Registro
{
    public int IdRegistro { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime FechaEjecucion { get; set; }
}
