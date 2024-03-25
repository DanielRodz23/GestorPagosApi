using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Models.LoginModel;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GestorPagosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogInController : ControllerBase
    {
        public LogInController(RepositoryUsuarios repositoryUsuarios, IMapper mapper, IConfiguration configuration)
        {
            this.repositoryUsuarios = repositoryUsuarios;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        private readonly RepositoryUsuarios repositoryUsuarios;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginModel model)
        {
            var data = await repositoryUsuarios.LogIn(model);
            if (data == null)
            {
                return BadRequest(new {mensaje="Contraseña incorrecta o usuario inexistente"});
            }

            var usr = mapper.Map<UsuarioDTO>(data);
            
            var jwt = configuration.GetSection("Jwt").Get<JwtModel>();

            var claims = new []{
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("rol", usr.IdRolNavigation.NombreRol),
                new Claim("id", usr.IdUsuario.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                jwt.Issuer, 
                jwt.Audience, 
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signin
                );
            UserDTOToken usrtoken = new(){ TokenString = new JwtSecurityTokenHandler().WriteToken(token)};
            return Ok(usrtoken);
        }
    }
}