using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Features.Logistica.Queries;
using TuyaPruebaGerman.Application.Interfaces;
using TuyaPruebaGerman.Application.Interfaces.Repositories;
using TuyaPruebaGerman.Application.Parameters;
using TuyaPruebaGerman.Domain.Entities;
using TuyaPruebaGerman.Infrastructure.Persistence.Contexts;
using TuyaPruebaGerman.Infrastructure.Persistence.Repository;


namespace TuyaPruebaGerman.Infrastructure.Persistence.Repositories
{
    public class LogisticaRepositoryAsync : GenericRepositoryAsync<Logistica>, ILogisticaRepositoryAsync
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Logistica> _logistica;
        private IDataShapeHelper<Logistica> _dataShaper;
        private readonly IMockService _mockData;

        public LogisticaRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Logistica> dataShaper, IMockService mockData) : base(dbContext)

        {
            _dbContext = dbContext;
            _logistica = dbContext.Set<Logistica>();
            _dataShaper = dataShaper;
            _mockData = mockData;
        }

        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedLogisticaReponseAsync(GetLogisticaQuery requestParameter)
        {
            var LogisticaNumber = requestParameter.LogisticaNumber;
            var LogisticaName = requestParameter.LogisticaName;

            var pageNumber = requestParameter.PageNumber;
            var pageSize = requestParameter.PageSize;
            var orderBy = requestParameter.OrderBy;
            var fields = requestParameter.Fields;

            int recordsTotal, recordsFiltered;

            // Setup IQueryable
            var result = _logistica
                .AsNoTracking()
                .AsExpandable();

            // Count records total
            recordsTotal = await result.CountAsync();

            // filter data
            FilterByColumn(ref result, LogisticaNumber, LogisticaName);

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
                result = result.Select<Logistica>("new(" + fields + ")");
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

        private void FilterByColumn(ref IQueryable<Logistica> result, string nombreCia, string direccion)
        {

            if (string.IsNullOrEmpty(nombreCia) && string.IsNullOrEmpty(direccion))
                return;

            var predicate = PredicateBuilder.New<Logistica>();

            if (!string.IsNullOrEmpty(nombreCia))
                predicate = predicate.Or(p => p.NombreCompania.Contains(nombreCia.Trim()));

            if (!string.IsNullOrEmpty(direccion))
                predicate = predicate.Or(p => p.Direccion.Contains(direccion.Trim()));

            result = result.Where(predicate);
        }
    }
}
