﻿using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Models.Entities;
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
        private readonly RepositoryJugadores repositoryJugadores;
        private readonly IMapper mapper;

        public PagosController(RepositoryPagos repository, RepositoryJugadores repositoryJugadores, IMapper mapper)
        {
            this.repository = repository;
            this.repositoryJugadores = repositoryJugadores;
            this.mapper = mapper;
        }
        [HttpGet("{id}")]
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

            var jugador = repositoryJugadores.Get(pago.IdJugador);
            if (jugador == null)
            {
                return NotFound(new { Mensaje = "Jugador inexistente" });
            }
            if (jugador.Deuda < pago.CantidadPago)
            {
                return BadRequest(new
                {
                    Mensaje = "EL pago solicitado es mayor que la deuda.\n" +
                    $"La deuda es de: {jugador.Deuda}"
                });
            }

            //Si todo es correcto
            jugador.Deuda = jugador.Deuda - pago.CantidadPago;
            var newpago = mapper.Map<Pago>(pago);

            repositoryJugadores.Update(jugador);
            repository.Insert(newpago);
            var pagorealizado = mapper.Map<PagoDTO>(newpago);
            //Regresa el pago
            //return Ok(pagorealizado);

            //Regresa el jugador
            var newjug = await repositoryJugadores.GetPagosInclude(pago.IdJugador);
            var jugadorDTO = mapper.Map<JugadorDTO>(newjug);
            return Ok(jugadorDTO);
        }
    }
}
