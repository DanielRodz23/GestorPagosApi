using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    }
}