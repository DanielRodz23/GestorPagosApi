using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Identity;
using GestorPagosApi.Models.Entities;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorPagosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly Repository<Categoria> repository;
        private readonly IMapper mapper;

        public CategoriasController(Repository<Categoria> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var cats = repository.GetAll();
            if (cats == null) { return NotFound(); }
            var data = mapper.Map<IEnumerable<CategoriaDTO>>(cats);
            return Ok(data);

        }
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpPost]
        public async Task<IActionResult> PostCataegoria(CategoriaDTO categoria)
        {
            if (categoria == null)
            {
                return BadRequest();
            }
            var datos = mapper.Map<Categoria>(categoria);
            repository.Insert(datos);
            var catdto = mapper.Map<CategoriaDTO>(datos);
            return Created("",catdto);
        }
    }
}
