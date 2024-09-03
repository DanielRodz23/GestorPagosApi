using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Models.Entities;
using GestorPagosApi.Models.LoginModel;
using GestorPagosApi.Models.ViewModels.AdminViewModels;
using GestorPagosApi.Models.ViewModels.ResponsableViewModels;

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

            CreateMap<Pago, PagoDTO>();
            CreateMap<PagoDTO, Pago>();

            CreateMap<LoginModel, LogInDTO>();
            CreateMap<LogInDTO, LoginModel>();

            CreateMap<Temporada, TemporadaDTO>();
            CreateMap<TemporadaDTO, Temporada>();

            CreateMap<Pago, DashPago>()
                .ForMember(dest => dest.JugadorNavigation, opt => opt.MapFrom(src => src.IdJugadorNavigation.Nombre))
                .ForMember(dest => dest.ResponsableNavigation, opt => opt.MapFrom(src => src.IdResponsableNavigation.Nombre));

            CreateMap<Jugador, DashJugador>()
                .ForMember(dest => dest.CategoriaNavigation, opt => opt
                .MapFrom(src => src.IdCategoriaNavigation.NombreCategoria));

            CreateMap<Jugador, ViewJugadoresAdmin>()
                .ForMember(dest => dest.NombreCategoria, opt => opt.MapFrom(src => src.IdCategoriaNavigation.NombreCategoria));

            CreateMap<Usuarios, ViewResponsablesAdmin>();

            CreateMap<Temporada, ViewTemporadasAdmin>();

            CreateMap<Pago, HistorialPagos>();
        }
    }
}
