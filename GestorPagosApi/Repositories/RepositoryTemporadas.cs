using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorPagosApi.Models.Entities;

namespace GestorPagosApi.Repositories
{
    public class RepositoryTemporadas : Repository<Temporada>
    {
        public RepositoryTemporadas(ClubDeportivoContext ctx) : base(ctx)
        {
        }
    }
}