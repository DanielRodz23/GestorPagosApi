using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace GestorPagosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly RepositoryPagos repository;
        private readonly IMapper mapper;

        public PagosController(RepositoryPagos repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetPago(int id)
        {
            var pago = repository.Get(id);
            if (pago == null)
            {
                return NotFound();
            }
            var dato = mapper.Map<PagoDTO>(pago);
            return Ok(dato);
        }
        [HttpPost]
        public async Task<IActionResult> PostPagar(PagoDTO pago)
        {
            if (pago == null) return NotFound();

            var jugador = await repository.ctx.Jugador.FirstOrDefaultAsync(x => x.IdJugador == pago.IdJugador);
            if (jugador == null)
            {
                return NotFound(new { Mensaje = "Jugador no encontrado" });
            }
            if (jugador.Deuda < pago.CantidadPago)
            {
                return BadRequest(pago);
            }
            return null;
        }
    }
}
