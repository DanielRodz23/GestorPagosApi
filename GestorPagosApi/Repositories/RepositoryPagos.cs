using GestorPagosApi.DTOs;
using GestorPagosApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorPagosApi.Repositories
{
    public class RepositoryPagos : Repository<Pago>
    {
        public RepositoryPagos(ClubDeportivoContext ctx) : base(ctx){}
        public async Task<IEnumerable<Pago>> GetPagosByJugador(int id)
        {
            //return ctx.Pago.Where(x=>x.)
            return await ctx.Pago.Where(x=>x.IdJugador==id).ToListAsync();
        }
        public async Task<Usuarios?> GetPagosByResponsable(int id){
            //Regresar una lista de jugadores que incluya los pagos
            return await ctx.Usuarios.Include(x=>x.Jugador).ThenInclude(x=>x.Pago).Where(x=>x.IdUsuario==id).FirstOrDefaultAsync();
            //return ctx.Usuarios.Include(x=>x.)
        }
    }
}
