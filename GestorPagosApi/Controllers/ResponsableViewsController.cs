using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Identity;
using GestorPagosApi.Models.Entities;
using GestorPagosApi.Models.ViewModels.ResponsableViewModels;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorPagosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = IdentityData.ResponsableUserPolicyName)]
    public class ResponsableViewsController : ControllerBase
    {
        private readonly RepositoryUsuarios repositoryUsuarios;
        private readonly RepositoryTemporadas repositoryTemporadas;
        private readonly IMapper mapper;

        public ResponsableViewsController(RepositoryUsuarios repositoryUsuarios, RepositoryTemporadas repositoryTemporadas, IMapper mapper)
        {
            this.repositoryUsuarios = repositoryUsuarios;
            this.repositoryTemporadas = repositoryTemporadas;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var context = HttpContext;
            var claim = User.FindFirst("id");
            if (claim == null) return BadRequest();
            string id = claim.Value;
            Usuarios? user = await repositoryUsuarios.GetUserIncludeJugadoresPagosAsync(int.Parse(id));
            if (user==null) return NotFound();
            var jugs = user.Jugador.ToList() ;
            var costototal = user.Jugador.Select(x => x.IdTemporadaNavigation.Costo).Sum();
            var saldopagado = user.Jugador.Select(x => x.IdTemporadaNavigation.Costo - x.Deuda).Sum();
            var listapagos = user.Pago.Select(x => mapper.Map<HistorialPagos>(x)).ToList();
            DashboardResponsableViewModel vm = new DashboardResponsableViewModel()
            {
                costoTotalTemporada = costototal,
                saldoPagado = saldopagado,
                listaPagos = listapagos ,
            };
            vm.saldoPendiente = vm.costoTotalTemporada-vm.saldoPagado;
            return Ok(vm);
        }
    }
}