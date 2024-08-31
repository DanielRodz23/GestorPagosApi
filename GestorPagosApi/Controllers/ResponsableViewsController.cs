using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestorPagosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResponsableViewsController : ControllerBase
    {
        private readonly RepositoryUsuarios repositoryUsuarios;
        public ResponsableViewsController(RepositoryUsuarios repositoryUsuarios)
        {
            this.repositoryUsuarios = repositoryUsuarios;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var context = HttpContext;
            var claim = User.FindFirst("id");
            if (claim==null) return BadRequest();
            string id = claim.Value;
            var user = repositoryUsuarios.Get(int.Parse(id));
            return Ok();
        }
    }
}