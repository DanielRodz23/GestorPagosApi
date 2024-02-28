using GestorPagosApi.Models.Entities;

namespace GestorPagosApi.Repositories
{
    public class RepositoryPagos : Repository<Pago>
    {
        public RepositoryPagos(ClubDeportivoContext ctx) : base(ctx){}


    }
}
