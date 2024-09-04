using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorPagosApi.Models.ViewModels.TesoreroViewModels;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestorPagosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TesoreroController : ControllerBase
    {
        public readonly RepositoryJugadores repositoryJugadores;
        public TesoreroController(RepositoryJugadores repositoryJugadores)
        {
            this.repositoryJugadores = repositoryJugadores;
        }
        [HttpGet]
        public IActionResult Get()
        {
            TesoreroIndexViewModel vm = new();
            vm.ListaPersonas = repositoryJugadores.GetAll().Select(x=>new Personas{Id=x.IdJugador, Nombre = x.Nombre}).ToList();
            return Ok(vm);
        }
        [HttpGet("SaldoPendiente/{id}")]
        public async Task<IActionResult> GetSaldoPendiente(int id)
        {
            var jugador = repositoryJugadores.Get(id);
            if (jugador ==null)
                return NotFound();
            return Ok(jugador.Deuda);
        }       
    }
}