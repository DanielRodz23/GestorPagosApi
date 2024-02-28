using GestorPagosApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestorPagosApi.Repositories
{
    public class RepositoryJugadores : Repository<Jugador>
    {
        public RepositoryJugadores(ClubDeportivoContext ctx) : base(ctx)
        {
        }
        public async Task<IEnumerable<Jugador>> GetAllAsync()
        {
            return await ctx.Jugador.OrderBy(x => x.Nombre).ToListAsync();
        }
    }
}
