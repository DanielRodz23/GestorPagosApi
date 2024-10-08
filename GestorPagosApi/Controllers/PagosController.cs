﻿using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Identity;
using GestorPagosApi.Models.Entities;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorPagosApi.Controllers
{
    // [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    // [Authorize(Policy = IdentityData.TesoreroUserPolicyName)]
    // [Authorize(Policy = IdentityData.ResponsableUserPolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly RepositoryPagos repository;
        private readonly RepositoryJugadores repositoryJugadores;
        private readonly IMapper mapper;

        public PagosController(RepositoryPagos repository, RepositoryJugadores repositoryJugadores, IMapper mapper)
        {
            this.repository = repository;
            this.repositoryJugadores = repositoryJugadores;
            this.mapper = mapper;
        }
        [HttpGet("PagosByResponsable/{id}")]
        public async Task<IActionResult> GetPagosByResponsable(int id)
        {
            //Obtiene el usuario con include de los jugadores y sus pagos
            var datos = await repository.GetPagosByResponsable(id);
            if (datos == null) return NotFound();

            //Convierte la consulta en un dto de usuario
            var user = mapper.Map<UsuarioDTO>(datos);
            return Ok(user);
        }
        [HttpGet("PagosByJugador/{id}")]
        public async Task<IActionResult> GetPagosByJugador(int id)
        {
            var datos = await repository.GetPagosByJugador(id);
            if (datos == null) return NotFound();

            var pagos = mapper.Map<IEnumerable<PagoDTO>>(datos);
            return Ok(pagos);
        }
        [HttpGet("Pago/{id}")]
        public async Task<IActionResult> GetPago(int id)
        {
            var pago = repository.Get(id);
            if (pago == null)
            {
                return NotFound(new { mensaje = $"Pago con Id: {id} no existe en la base de datos" });
            }
            var dato = mapper.Map<PagoDTO>(pago);
            return Ok(dato);
        }
        [HttpPost("Pago")]
        public async Task<IActionResult> PostPago(PagoDTO pago)
        {
            Jugador jugador = repositoryJugadores.Get(pago.idJugador);
            if (jugador == null) return NotFound();

            if (jugador.Deuda<pago.cantidadPago)
                return BadRequest("La cantidad de pago sobrepasa la deuda");
            
            jugador.Deuda-=pago.cantidadPago;
            
            Pago newpago = new();
            newpago.IdJugador = pago.idJugador;
            newpago.CantidadPago = pago.cantidadPago;
            newpago.IdTemporada = jugador.IdTemporada;
            newpago.IdResponsable = jugador.IdUsuario;
            newpago.FechaPago = DateTime.UtcNow;
            repository.Insert(newpago);
            repositoryJugadores.Update(jugador);
            return Ok();

        }
        [HttpPost]
        public async Task<IActionResult> PostPagar(PagoDTO pago)
        {
            var context = HttpContext;
            string id = User.FindFirst("id").Value;

            if (pago == null) return NotFound();

            var jugador = repositoryJugadores.Get(pago.idJugador);
            if (jugador == null)
            {
                return NotFound(new { Mensaje = "Jugador inexistente" });
            }
            if (jugador.Deuda < pago.cantidadPago)
            {
                return BadRequest(new
                {
                    Mensaje = "EL pago solicitado es mayor que la deuda.\n" +
                    $"La deuda es de: {jugador.Deuda}"
                });
            }

            //Si todo es correcto
            jugador.Deuda -= pago.cantidadPago;
            pago.idResponsable = jugador.IdUsuario;
            var newpago = mapper.Map<Pago>(pago);

            repositoryJugadores.Update(jugador);
            repository.Insert(newpago);
            var pagorealizado = mapper.Map<PagoDTO>(newpago);
            //Regresa el pago
            //return Ok(pagorealizado);

            //Regresa el jugador
            var newjug = await repositoryJugadores.GetPagosInclude(pago.idJugador);
            var jugadorDTO = mapper.Map<JugadorDTO>(newjug);
            return Ok(jugadorDTO);
        }
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = repository.Get(id);
            if (pago == null)
            {
                return NotFound();
            }
            repository.Delete(pago);
            return Ok();
        }
    }
}
