using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Models.Entities;
using GestorPagosApi.Repositories;
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
        [HttpPost]
        public async Task<IActionResult> PostCtaegoria(CategoriaDTO categoria)
        {
            if (categoria == null)
            {
                return NotFound();
            }
            var datos = mapper.Map<Categoria>(categoria);
            repository.Insert(datos);

            return Ok(datos);
        }
    }
}
