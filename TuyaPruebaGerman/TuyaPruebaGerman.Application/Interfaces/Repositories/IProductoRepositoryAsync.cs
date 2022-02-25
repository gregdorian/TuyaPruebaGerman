using System.Collections.Generic;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Features.Productos.Queries.GetProductos;
using TuyaPruebaGerman.Application.Parameters;
using TuyaPruebaGerman.Domain.Entities;

namespace TuyaPruebaGerman.Application.Interfaces.Repositories
{
    public interface IProductoRepositoryAsync : IGenericRepositoryAsync<Producto>
    {
        Task<bool> IsProductoDescriptionAsync(string descripcion);

        Task<bool> SeedDataAsync(int rowCount);

        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedProductoReponseAsync(GetProductosQuery requestParameters);

    }
}
