using System.Collections.Generic;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Features.Clientes.Queries;
using TuyaPruebaGerman.Application.Parameters;
using TuyaPruebaGerman.Domain.Entities;

namespace TuyaPruebaGerman.Application.Interfaces.Repositories
{
    public interface IClienteRepositoryAsync : IGenericRepositoryAsync<Cliente>
    {
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedClienteReponseAsync(GetClientesQuery requestParameter);
    }
}
