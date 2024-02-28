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
            CreateMap<UsuarioDTO, Usuarios>();

            CreateMap<Roles, RolDTO>();
            CreateMap<RolDTO, Roles>();

            CreateMap<Jugador, JugadorDTO>();
            CreateMap<JugadorDTO, Jugador>();

            CreateMap<Categoria, CategoriaDTO>();
            CreateMap<CategoriaDTO, Categoria>();

        }
    }
}
