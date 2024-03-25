using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Models.Entities;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorPagosApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JugadoresController : ControllerBase
    {
        private readonly RepositoryJugadores repository;
        private readonly IMapper mapper;

        public JugadoresController(RepositoryJugadores repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetJugadores()
        {
            var proye = await repository.GetAllAsync();
            if (proye == null) { return NotFound(); }
            var datos = mapper.Map<IEnumerable<JugadorDTO>>(proye);
            
            return Ok(datos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJugadoresDeUsuario(int id)
        {
            var data = await repository.GetJugadoresDeUsuario(id);
            if (data == null) return BadRequest(new {mensaje = "No existe un responsable con ese ID"});
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> PostJugadores(JugadorDTO jugador){
            if (jugador==null)
            {
                return BadRequest(new {mensaje = "Contenido incorrecto"});
            }

            var jug = mapper.Map<Jugador>(jugador);
            repository.Insert(jug);
            var returndata = mapper.Map<JugadorDTO>(jug);
            return Ok(returndata);
        }
        [HttpPut]
        public async Task<IActionResult> PutJugadores(JugadorDTO jugador)
        {
            if (jugador==null)
            {
                return BadRequest(new { mensaje = "Contenido incorrecto"});                
            }
            var dato = mapper.Map<Jugador>(jugador);
            repository.Update(dato);
            var returdata = mapper.Map<JugadorDTO>(dato);
            return Ok(returdata);
        }
        
    }
}
