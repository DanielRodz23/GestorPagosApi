namespace GestorPagosApi.Models.ViewModels.AdminViewModels
{
    public class ViewResponsablesAdmin
    {
        //IdResponsable
        public int idUsuario { get; set; }
        //Nombre
        public string nombre { get; set; } = null!;
        //Telefono
        public string? telefono { get; set; }
        //Correo
        public string? correo { get; set; }

    }
}
