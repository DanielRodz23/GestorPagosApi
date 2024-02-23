
namespace GestorPagosApi.DTOs
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; } = null!;

        public string Usuario { get; set; } = null!;

        public string Contrasena { get; set; } = null!;

        public int IdRol { get; set; }

        public RolDTO IdRolNavigation { get; set; } = new();
        public ICollection<JugadorDTO> Jugador { get; set; } = new List<JugadorDTO>();

    }
}
