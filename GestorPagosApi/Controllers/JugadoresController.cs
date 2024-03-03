using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorPagosApi.Controllers
{
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
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetJugadoresDeUsuario(int id)
        {
            var data = await repository.GetJugadoresDeUsuario(id);
            if (data == null) return BadRequest(new {mensaje = "No existe un responsable con ese ID"});
            return Ok(data);
        }
    }
}
