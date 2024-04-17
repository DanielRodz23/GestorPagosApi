using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorPagosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemporadasController : ControllerBase
    {
        private readonly RepositoryTemporadas repository;
        private readonly IMapper mapper;

        public TemporadasController(RepositoryTemporadas repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper=mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetTemporadasActuales(){
            var temps = await repository.GetTemporadasActualesAsync();
            var tempsdto = mapper.Map<IEnumerable<TemporadaDTO>>(temps);
            return Ok(tempsdto);
        }
    }
}