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
            DashboardModel model = new()
            {
                Totaljugadores = repositoryJugadores.GetAll().Count(),
                TotalTemporadas = repositoryTemporadas.GetAll().Count(),
                TotalCategorias = repositoryCategorias.GetAll().Count(),
                TotalPagos = repositoryPagos.GetAll().Count(),
                ListaPagos = mapper.Map<IEnumerable<DashPago>>(repositoryPagos.GetCuatroPagos()).ToList(),
                ListaJugadores = mapper.Map<IEnumerable<DashJugador>>(repositoryJugadores.GetCuatroJugadores()).ToList()
            };

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
            var jugs = await repositoryJugadores.GetAllIncludeCategoriasAsync();
            var jugsmapd = mapper.Map<IEnumerable<ViewJugadoresAdmin>>(jugs);
            return Ok(jugsmapd);
        }
        [HttpGet("VerResponsables")]
        public async Task<IActionResult> GetViewResponsables()
        {
            var resp = await repositoryUsuarios.GetAllUsuariosTipoResponsable();
            var respsmapd = mapper.Map<IEnumerable<ViewResponsablesAdmin>>(resp);
            return Ok(respsmapd);
        }
        [HttpGet("VerTemporadas")]
        public async Task<IActionResult> GetViewTemporadas()
        {
            var temps = await repositoryTemporadas.GetTemporadasActualesAsync();
            var tempsmapd = mapper.Map<IEnumerable< ViewTemporadasAdmin>>(temps);
            return Ok(tempsmapd);
        }
    }
}
