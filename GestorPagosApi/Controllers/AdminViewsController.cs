using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Identity;
using GestorPagosApi.Models.Entities;
using GestorPagosApi.Models.ViewModels;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorPagosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminViewsController : ControllerBase
    {
        private readonly RepositoryJugadores repositoryJugadores;
        private readonly RepositoryTemporadas repositoryTemporadas;
        private readonly Repository<Categoria> repositoryCategorias;
        private readonly RepositoryPagos repositoryPagos;
        private readonly Mapper mapper;

        public AdminViewsController(
            RepositoryJugadores repositoryJugadores, 
            RepositoryTemporadas repositoryTemporadas, 
            Repository<Categoria> repositoryCategorias, 
            RepositoryPagos repositoryPagos,
            Mapper mapper)
        {
            this.repositoryJugadores = repositoryJugadores;
            this.repositoryTemporadas = repositoryTemporadas;
            this.repositoryCategorias = repositoryCategorias;
            this.repositoryPagos = repositoryPagos;
            this.mapper = mapper;
        }
        //[Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            DashboardModel model = new DashboardModel();
            model.Totaljugadores =  repositoryJugadores.GetAll().Count();
            model.TotalTemporadas =  repositoryTemporadas.GetAll().Count();
            model.TotalCategorias = repositoryCategorias.GetAll().Count();
            model.TotalPagos = repositoryPagos.GetAll().Count();
            model.ListaPagos = mapper.Map<IEnumerable<PagoDTO>>(repositoryPagos.GetCuatroPagos()).ToList();
            model.ListaJugadores = mapper.Map<IEnumerable<JugadorDTO>>(repositoryJugadores.GetCuatroJugadores()).ToList();

            return Ok(model);
        }
    }
}
