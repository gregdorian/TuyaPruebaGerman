using System.Collections.Generic;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Features.Logistica.Queries;
using TuyaPruebaGerman.Application.Parameters;
using TuyaPruebaGerman.Domain.Entities;

namespace TuyaPruebaGerman.Application.Interfaces.Repositories
{
    public interface ILogisticaRepositoryAsync : IGenericRepositoryAsync<Logistica>
    {
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedLogisticaReponseAsync(GetLogisticaQuery requestParameter);
    }
}
