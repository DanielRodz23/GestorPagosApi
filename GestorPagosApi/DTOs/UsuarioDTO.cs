
namespace GestorPagosApi.DTOs
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; } = null!;

        public string Usuario { get; set; } = null!;

        public int IdRol { get; set; }

        public RolDTO? IdRolNavigation { get; set; } 
        public ICollection<JugadorDTO>? Jugador { get; set; }

    }
}
