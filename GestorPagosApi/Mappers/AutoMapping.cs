using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Models.Entities;

namespace GestorPagosApi.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            //CreateMap<IEnumerable<Usuarios>, IEnumerable<UsuarioDTO>>();
            CreateMap<Usuarios, UsuarioDTO>();
            CreateMap<Roles, RolDTO>();
            CreateMap<Jugador, JugadorDTO>();
        }
    }
}
