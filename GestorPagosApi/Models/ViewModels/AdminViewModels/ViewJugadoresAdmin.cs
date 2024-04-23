namespace GestorPagosApi.Models.ViewModels.AdminViewModels
{
    public class ViewJugadoresAdmin
    {
        public int IdJugador { get; set; }
        public string Nombre { get; set; } = null!;
        public DateOnly Dob { get; set; }
        public string NombreCategoria { get; set; } = null!;

    }
}
