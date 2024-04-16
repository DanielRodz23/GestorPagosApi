using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        public LogInController(RepositoryUsuarios repositoryUsuarios, IMapper mapper)
        {
            this.repositoryUsuarios = repositoryUsuarios;
            this.mapper = mapper;
        }
        private readonly RepositoryUsuarios repositoryUsuarios;
        private readonly IMapper mapper;

        JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginModel model)
        {
            var data = await repositoryUsuarios.LogIn(model);
            if (data == null)
            {
                return BadRequest(new {mensaje="Contraseña incorrecta o usuario inexistente"});
            }

            var usr = mapper.Map<UsuarioDTO>(data);
            
            var configuration = new ConfigurationBuilder()
    .AddJsonFile("jwtsettings.json")
    .Build();

            var jwt = configuration.GetSection("Jwt").Get<JwtModel>();

            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", usr.IdUsuario.ToString())
            };
            if (data.IdRolNavigation.NombreRol==IdentityData.AdminUserClaimName)
            {
                claims.Add(new Claim(IdentityData.AdminUserClaimName, "true"));  
            }
            else if (data.IdRolNavigation.NombreRol==IdentityData.TesoreroUserClaimName)
            {
                claims.Add(new Claim(IdentityData.TesoreroUserClaimName, "true"));  
            }
            else if (data.IdRolNavigation.NombreRol==IdentityData.ResponsableUserClaimName)
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
            var tokendescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(120),
                Issuer = jwt.Issuer,
                Audience = jwt.Audience,
                SigningCredentials = signin
            };
            var token = TokenHandler.CreateToken(tokendescriptor);
            var response = TokenHandler.WriteToken(token);
            UserDTOToken usrtoken = new(){ TokenString = response};
            return Ok(usrtoken);
        }
    }
}