using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Identity;
using GestorPagosApi.Models.Entities;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
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
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpGet("JustUsers")]
        public async Task<IActionResult> GetSoloUsuarios()
        {
            var proye =  repository.GetAll();
            var data = mapper.Map<IEnumerable<UsuarioDTO>>(proye);
            return Ok(data);
        }
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarios(int id)
        {
            var user = await repository.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var data = mapper.Map<UsuarioDTO>(user);
            return Ok(data);
        }
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpPost]
        public async Task<IActionResult> PostUsuarios(UsuarioDTO usuarios)
        {
            if (usuarios == null)
            {
                return NotFound();
            }
            var data = mapper.Map<Usuarios>(usuarios);
            repository.Insert(data);
            return Ok(data);
        }
    }
}
