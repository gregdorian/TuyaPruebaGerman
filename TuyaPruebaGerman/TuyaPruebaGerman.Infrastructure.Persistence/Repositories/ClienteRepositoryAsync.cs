using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Features.Clientes.Queries;
using TuyaPruebaGerman.Application.Interfaces;
using TuyaPruebaGerman.Application.Interfaces.Repositories;
using TuyaPruebaGerman.Application.Parameters;
using TuyaPruebaGerman.Domain.Entities;
using TuyaPruebaGerman.Infrastructure.Persistence.Contexts;
using TuyaPruebaGerman.Infrastructure.Persistence.Repository;

namespace TuyaPruebaGerman.Infrastructure.Persistence.Repositories
{
    public class ClienteRepositoryAsync : GenericRepositoryAsync<Cliente>, IClienteRepositoryAsync
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Cliente> _cliente;
        private IDataShapeHelper<Cliente> _dataShaper;
        private readonly IMockService _mockData;
        public ClienteRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Cliente> dataShaper, IMockService mockData) : base(dbContext)

        {
            _dbContext = dbContext;
            _cliente = dbContext.Set<Cliente>();
            _dataShaper = dataShaper;
            _mockData = mockData;
        }
        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedClienteReponseAsync(GetClientesQuery requestParameter)
        {
            var clienteNumber = requestParameter.ClienteNumber;
            var clienteName = requestParameter.ClienteName;

            var pageNumber = requestParameter.PageNumber;
            var pageSize = requestParameter.PageSize;
            var orderBy = requestParameter.OrderBy;
            var fields = requestParameter.Fields;

            int recordsTotal, recordsFiltered;

            // Setup IQueryable
            var result = _cliente
                .AsNoTracking()
                .AsExpandable();

            // Count records total
            recordsTotal = await result.CountAsync();

            // filter data
            FilterByColumn(ref result, clienteNumber, clienteName);

            // Count records after filter
            recordsFiltered = await result.CountAsync();

            //set Record counts
            var recordsCount = new RecordsCount
            {
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };

            // set order by
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                result = result.OrderBy(orderBy);
            }

            // select columns
            if (!string.IsNullOrWhiteSpace(fields))
            {
                result = result.Select<Cliente>("new(" + fields + ")");
            }
            // paging
            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // retrieve data to list
            var resultData = await result.ToListAsync();
            // shape data
            var shapeData = _dataShaper.ShapeData(resultData, fields);

            return (shapeData, recordsCount);
        }


        private void FilterByColumn(ref IQueryable<Cliente> clientes, string clienteApellido, string clienteName)
        {
            //if (!cliente.Any())
            //    return;

            if (string.IsNullOrEmpty(clienteName) && string.IsNullOrEmpty(clienteApellido))
                return;

            var predicate = PredicateBuilder.New<Cliente>();

            if (!string.IsNullOrEmpty(clienteName))
                predicate = predicate.Or(p => p.Nombres.Contains(clienteName.Trim()));

            if (!string.IsNullOrEmpty(clienteApellido))
                predicate = predicate.Or(p => p.Apellidos.Contains(clienteApellido.Trim()));

            clientes = clientes.Where(predicate);
        }
    }
}
