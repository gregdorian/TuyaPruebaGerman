using System.Collections.Generic;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Features.OrdenCompra.Queries;
using TuyaPruebaGerman.Application.Parameters;
using TuyaPruebaGerman.Domain.Entities;

namespace TuyaPruebaGerman.Application.Interfaces.Repositories
{
    public interface IOrdenCompraRepositoryAsync : IGenericRepositoryAsync<OrdenCompra>
    {
        Task<bool> IsOrdenCompranNumberAsync(int OrdenNumer);

        //Task<bool> SeedDataAsync(int rowCount);

        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedOrdenCompraReponseAsync(GetOrdenCompraQuery requestParameters);
    }
}
