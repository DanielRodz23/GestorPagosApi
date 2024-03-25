using GestorPagosApi.DTOs;
using GestorPagosApi.Models.Entities;
using GestorPagosApi.Models.LoginModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorPagosApi.Repositories
{
    public class RepositoryUsuarios : Repository<Usuarios>
    {
        public RepositoryUsuarios(ClubDeportivoContext ctx) : base(ctx)
        {
        }
        public async Task<IEnumerable<Usuarios>> GetAllUsuariosInclude()
        {
            return await ctx.Usuarios.OrderBy(x => x.Nombre).Include(x => x.IdRolNavigation).Include(x => x.Jugador).ToListAsync();
            //return await ctx.Usuarios.OrderBy(x => x.Nombre)
            //    .Include(x => x.IdRolNavigation)
            //    .Include(x => x.Jugador)
            //    .Select(x => new UsuarioDTO()
            //    {
            //        Nombre = x.Nombre,
            //        IdUsuario = x.IdUsuario,
            //        Usuario = x.Usuario,
            //        Contrasena = x.Contrasena,
            //        IdRol = x.IdRol,
            //        IdRolNavigation = new RolDTO() { IdRol = x.IdRolNavigation.IdRol, NombreRol = x.IdRolNavigation.NombreRol },
            //        Jugador = x.Jugador.Select(z => new JugadorDTO()
            //        {
            //            IdJugador = z.IdJugador,
            //            Nombre = z.Nombre,
            //            Dob = z.Dob,
            //            IdUsuario = z.IdUsuario,
            //            IdTemporada = z.IdTemporada ?? 0,
            //            Deuda = z.Deuda
            //        }).ToArray()

            //    })
            //    .ToListAsync();
        }
        public async Task<Usuarios?> GetAsync(int id)
        {
            return await ctx.Usuarios
                .Include(x=>x.Jugador)
                .Include(x=>x.IdRolNavigation)
                .FirstOrDefaultAsync(x=>x.IdUsuario==id);
        }

        public async Task<Usuarios> LogIn(LoginModel model)
        {
            return await ctx.Usuarios.Include(x=>x.IdRolNavigation).FirstOrDefaultAsync(x=>x.Usuario == model.Usuario && x.Contrasena==model.Contrasena);
        }
    }
}
