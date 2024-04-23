using GestorPagosApi.Models.Entities;

namespace GestorPagosApi.Repositories
{
    public class RepositoryTemporadas : Repository<Temporada>
    {
        public RepositoryTemporadas(ClubDeportivoContext ctx) : base(ctx)
        {
        }
        public async Task<IEnumerable<Temporada>> GetTemporadasActualesAsync()
        {
            return ctx.Temporada.Where(x => x.TempActual == true);
        }
        public async Task<IEnumerable<Temporada>> GetCuatroTemporadasAsync()
        {
            return ctx.Temporada.Take(4);
        }
    }
}