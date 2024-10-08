using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestorPagosApi.DTOs;
using GestorPagosApi.Identity;
using GestorPagosApi.Models.Entities;
using GestorPagosApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorPagosApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TemporadasController : ControllerBase
    {
        private readonly RepositoryTemporadas repository;
        private readonly IMapper mapper;

        public TemporadasController(RepositoryTemporadas repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetTemporadasActuales()
        {
            var temps = await repository.GetTemporadasActualesAsync();
            var tempsdto = mapper.Map<IEnumerable<TemporadaDTO>>(temps);
            return Ok(tempsdto);
        }
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpPost]
        public async Task<IActionResult> PostTemporada(TemporadaDTO temporada)
        {
            if (temporada == null)
            {
                return NotFound();
            }
            var temp = mapper.Map<Temporada>(temporada);
            repository.Insert(temp);
            var tmpdto = mapper.Map<TemporadaDTO>(temp);
            return Created("", tmpdto);        
        }

        [Authorize(IdentityData.AdminUserPolicyName)]
        [HttpPut]
        public async Task<IActionResult> PutTemporada(TemporadaDTO dTO){
            if (dTO == null) return BadRequest();
            //Validat DTO
            var  temp = repository.Get(dTO.idTemporada);
            if (temp == null)
            {
                return NotFound();
            }
            var tempmapd = mapper.Map<Temporada>(dTO);
            repository.Update(tempmapd);
            return Ok();
        }

        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemporada(int id){
            var temp = repository.Get(id);
            if (temp == null || temp.Exists==false)
                return NotFound();
            return Ok();
        }
    }
}