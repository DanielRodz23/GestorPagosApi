﻿using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Models.Entities;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace GestorPagosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly RepositoryUsuarios repository;
        private readonly IMapper mapper;

        public UsuariosController(RepositoryUsuarios repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        //{
        //    var datos = await repository.GetAllUsuariosInclude();

        //    return Ok(datos);
        //}

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var data = await repository.GetAllUsuariosInclude();
            if (data == null)
            {
                return NotFound();
            }
            var users = mapper.Map<IEnumerable<UsuarioDTO>>(data);
            //users = data.Select(x => new UsuarioDTO()
            //{
            //    Nombre = x.Nombre,
            //    IdUsuario = x.IdUsuario,
            //    Usuario = x.Usuario,
            //    Contrasena = x.Contrasena,
            //    IdRol = x.IdRol,
            //    IdRolNavigation = new RolDTO() { IdRol = x.IdRolNavigation.IdRol, NombreRol = x.IdRolNavigation.NombreRol },
            //    Jugador = x.Jugador.Select(z => new JugadorDTO()
            //    {
            //        IdJugador = z.IdJugador,
            //        Nombre = z.Nombre,
            //        Dob = z.Dob,
            //        IdUsuario = z.IdUsuario,
            //        IdTemporada = z.IdTemporada ?? 0,
            //        Deuda = z.Deuda
            //    }).ToList()
            //});
            return Ok(users);
        }
    }
}
