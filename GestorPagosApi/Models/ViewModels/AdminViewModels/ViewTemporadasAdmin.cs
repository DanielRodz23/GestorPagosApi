namespace GestorPagosApi.Models.ViewModels.AdminViewModels
{
    public class ViewTemporadasAdmin
    {
        public int idTemporada { get; set; }

        public string nombre { get; set; } = null!;

        public DateTime fechaInicio { get; set; }

        public DateTime fechaFinal { get; set; }
    }
}
