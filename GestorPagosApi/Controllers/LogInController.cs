using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Identity;
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
        public LogInController(RepositoryUsuarios repositoryUsuarios, IMapper mapper, TokenValidationParameters tknValidationParameters)
        {
            this.repositoryUsuarios = repositoryUsuarios;
            this.mapper = mapper;
            this.tknValidationParameters = tknValidationParameters;
        }
        private readonly RepositoryUsuarios repositoryUsuarios;
        private readonly IMapper mapper;
        private readonly TokenValidationParameters tknValidationParameters;
        JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginModel model)
        {
            var data = await repositoryUsuarios.LogIn(model);
            if (data == null)
            {
                return BadRequest(new { mensaje = "Contraseña incorrecta o usuario inexistente" });
            }
            UsuarioDTO usr;
            try
            {
                usr = mapper.Map<UsuarioDTO>(data);
            }
            catch (Exception)
            {
                return StatusCode(500, new { mensaje = "Problemas con mapper" });
            }

            var configuration = new ConfigurationBuilder()
    .AddJsonFile("jwtsettings.json")
    .Build();

            var jwt = configuration.GetSection("Jwt").Get<JwtModel>();

            if (jwt == null)
            {
                return StatusCode(500, new { mensaje = "No se encuentra el archivo JWT" });
            }

            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", usr.IdUsuario.ToString())
            };
            if (data.IdRolNavigation.NombreRol == IdentityData.AdminUserClaimName)
            {
                claims.Add(new Claim(IdentityData.AdminUserClaimName, "true"));
            }
            else if (data.IdRolNavigation.NombreRol == IdentityData.TesoreroUserClaimName)
            {
                claims.Add(new Claim(IdentityData.TesoreroUserClaimName, "true"));
            }
            else if (data.IdRolNavigation.NombreRol == IdentityData.ResponsableUserClaimName)
            {
                claims.Add(new Claim(IdentityData.ResponsableUserClaimName, "true"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            // var token = new JwtSecurityToken(
            //     jwt.Issuer, 
            //     jwt.Audience, 
            //     claims,
            //     expires: DateTime.Now.AddMinutes(10),
            //     signingCredentials: signin
            //     );
            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(120),
                Issuer = jwt.Issuer,
                Audience = jwt.Audience,
                SigningCredentials = signin
            };
            var token = TokenHandler.CreateToken(tokendescriptor);
            var response = TokenHandler.WriteToken(token);

            usr.Jugador = null;
            UserDTOToken usrtoken = new() { TokenString = response, Usuario = usr };
            return Ok(usrtoken);
        }
        [HttpPost("Validator")]
        public async Task<IActionResult> Validator(Validator validator)
        {
            try
            {
                SecurityToken securityToken;
                var valid = TokenHandler.ValidateToken(validator.token, tknValidationParameters, out securityToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(new {mensaje = ex.ToString()});
            }
        }
    }
}