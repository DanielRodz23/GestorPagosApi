using GestorPagosApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestorPagosApi.Repositories
{
    public class RepositoryJugadores : Repository<Jugador>
    {
        public RepositoryJugadores(ClubDeportivoContext ctx) : base(ctx)
        {
        }
        public async Task<IEnumerable<Jugador>> GetJugadoresDeUsuario(int id)
        {
            return await ctx.Jugador.Where(x=>x.IdUsuario == id).ToListAsync();
        }
        public async Task<Jugador> GetPagosInclude(int id)
        {
            return await ctx.Jugador.Include(x => x.Pago.OrderByDescending(y=>y.FechaPago)).FirstOrDefaultAsync(x => x.IdJugador == id);
        }
        public async Task<IEnumerable<Jugador>> GetAllAsync()
        {
            return await ctx.Jugador.OrderBy(x => x.Nombre).ToListAsync();
        }
        public IEnumerable<Jugador> GetCuatroJugadores()
        {
            return ctx.Jugador.Take(4);
        }
    }
}
