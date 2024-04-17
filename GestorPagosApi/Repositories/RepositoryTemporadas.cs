using GestorPagosApi.Models.Entities;

namespace GestorPagosApi.Repositories
{
    public class RepositoryTemporadas : Repository<Temporada>
    {
        public RepositoryTemporadas(ClubDeportivoContext ctx) : base(ctx)
        {
        }
        public async Task<IEnumerable<Temporada>> GetTemporadasActualesAsync(){
            var temps = ctx.Temporada.Where(x=>x.TempActual==true);
            return temps;
        }
    }
}