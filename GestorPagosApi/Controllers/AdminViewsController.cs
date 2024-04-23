using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Identity;
using GestorPagosApi.Models.Entities;
using GestorPagosApi.Models.ViewModels.AdminViewModels;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorPagosApi.Controllers
{
    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminViewsController : ControllerBase
    {
        private readonly RepositoryJugadores repositoryJugadores;
        private readonly RepositoryTemporadas repositoryTemporadas;
        private readonly Repository<Categoria> repositoryCategorias;
        private readonly RepositoryPagos repositoryPagos;
        private readonly RepositoryUsuarios repositoryUsuarios;
        private readonly IMapper mapper;

        public AdminViewsController(
            RepositoryJugadores repositoryJugadores,
            RepositoryTemporadas repositoryTemporadas,
            Repository<Categoria> repositoryCategorias,
            RepositoryPagos repositoryPagos,
            RepositoryUsuarios repositoryUsuarios,
            IMapper mapper)
        {
            this.repositoryJugadores = repositoryJugadores;
            this.repositoryTemporadas = repositoryTemporadas;
            this.repositoryCategorias = repositoryCategorias;
            this.repositoryPagos = repositoryPagos;
            this.repositoryUsuarios = repositoryUsuarios;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            DashboardModel model = new DashboardModel();
            model.Totaljugadores = repositoryJugadores.GetAll().Count();
            model.TotalTemporadas = repositoryTemporadas.GetAll().Count();
            model.TotalCategorias = repositoryCategorias.GetAll().Count();
            model.TotalPagos = repositoryPagos.GetAll().Count();
            model.ListaPagos = mapper.Map<IEnumerable<DashPago>>(repositoryPagos.GetCuatroPagos()).ToList();
            model.ListaJugadores = mapper.Map<IEnumerable<DashJugador>>(repositoryJugadores.GetCuatroJugadores()).ToList();

            return Ok(model);
        }
        [HttpGet("VerCategorias")]
        public async Task<IActionResult> GetViewCategorias()
        {
            var cats = repositoryCategorias.GetAll();
            var catsmapd = mapper.Map<IEnumerable<CategoriaDTO>>(cats);
            return Ok(catsmapd);
        }
        [HttpGet("VerJugadores")]
        public async Task<IActionResult> GetViewJugadores()
        {
            var jugs = await repositoryJugadores.GetCuatroJugadoresIncludeCategoriasAsync();
            var jugsmapd = mapper.Map<IEnumerable<ViewJugadoresAdmin>>(jugs);
            return Ok(jugsmapd);
        }
        [HttpGet("VerResponsables")]
        public async Task<IActionResult> GetViewResponsables()
        {
            var resp = await repositoryUsuarios.GetCuatroUsuariosTipoResponsable();
            var respsmapd = mapper.Map<IEnumerable<ViewResponsablesAdmin>>(resp);
            return Ok(respsmapd);
        }
        [HttpGet("VerTemporadas")]
        public async Task<IActionResult> GetViewTemporadas()
        {
            var temps = await repositoryTemporadas.GetCuatroTemporadasAsync();
            var tempsmapd = mapper.Map<IEnumerable< ViewTemporadasAdmin>>(temps);
            return Ok(tempsmapd);
        }
    }
}
