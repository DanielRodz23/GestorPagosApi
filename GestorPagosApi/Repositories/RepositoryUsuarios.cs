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
        }
        public async Task<Usuarios?> GetAsync(int id)
        {
            return await ctx.Usuarios
                .Include(x=>x.Jugador)
                .Include(x=>x.IdRolNavigation)
                .FirstOrDefaultAsync(x=>x.IdUsuario==id);
        }
        public async Task<IEnumerable< Usuarios>> GetAllUsuariosTipoResponsable()
        {
            return ctx.Usuarios.Where(x => x.IdRol == 2);
        }

        public async Task<Usuarios> LogIn(LoginModel model)
        {
            return await ctx.Usuarios.Include(x=>x.IdRolNavigation).FirstOrDefaultAsync(x=>x.Usuario == model.Usuario && x.Contrasena==model.Contrasena);
        }
    }
}
