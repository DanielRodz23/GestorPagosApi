
namespace GestorPagosApi.DTOs
{
    public class UsuarioDTO
    {
        public int idUsuario { get; set; }

        public string nombre { get; set; } = null!;

        public string usuario { get; set; } = null!;

        public string contrasena { get; set; } = null!;

        public int idRol { get; set; }

        public bool? exists { get; set; }

        public string? telefono { get; set; }

        public string? correo { get; set; }

        public string? rfc { get; set; }

        public RolDTO? IdRolNavigation { get; set; }

    }
}
