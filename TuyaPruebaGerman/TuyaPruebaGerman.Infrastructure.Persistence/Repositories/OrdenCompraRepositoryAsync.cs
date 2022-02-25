using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

using TuyaPruebaGerman.Application.Features.OrdenCompra.Queries;
using TuyaPruebaGerman.Application.Interfaces;
using TuyaPruebaGerman.Application.Interfaces.Repositories;
using TuyaPruebaGerman.Application.Parameters;
using TuyaPruebaGerman.Domain.Entities;
using TuyaPruebaGerman.Infrastructure.Persistence.Contexts;
using TuyaPruebaGerman.Infrastructure.Persistence.Repository;

namespace TuyaPruebaGerman.Infrastructure.Persistence.Repositories
{
    public class OrdenCompraRepositoryAsync : GenericRepositoryAsync<OrdenCompra>, IOrdenCompraRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<OrdenCompra> _ordencompra;
        private IDataShapeHelper<OrdenCompra> _dataShaper;
        private readonly IMockService _mockData;

        public OrdenCompraRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<OrdenCompra> dataShaper, IMockService mockData) : base(dbContext)
        {
            _dbContext = dbContext;
            _ordencompra = dbContext.Set<OrdenCompra>();
            _dataShaper = dataShaper;
            _mockData = mockData;
        }

        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedOrdenCompraReponseAsync(GetOrdenCompraQuery requestParameters)
        {
            var numeroOrden = requestParameters.NumberOrdenCompra;
            var total = requestParameters.Total;


            var pageNumber = requestParameters.PageNumber;
            var pageSize = requestParameters.PageSize;
            var orderBy = requestParameters.OrderBy;
            var fields = requestParameters.Fields;

            int recordsTotal, recordsFiltered;

            // Setup IQueryable
            var result = _ordencompra
                .AsNoTracking()
                .AsExpandable();

            // Count records total
            recordsTotal = await result.CountAsync();

            // filter data
            FilterByColumn(ref result, numeroOrden, total);

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
                result = result.Select<OrdenCompra>("new(" + fields + ")");
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


        private void FilterByColumn(ref IQueryable<OrdenCompra> orden, int numeroOrden, decimal total)
        {

            if (string.IsNullOrEmpty(numeroOrden.ToString()) && string.IsNullOrEmpty(total.ToString()))
                return;

            var predicate = PredicateBuilder.New<OrdenCompra>();

            if (!string.IsNullOrEmpty(numeroOrden.ToString()))
                predicate = predicate.Or(p => p.NumeroOrden.ToString().Contains(numeroOrden.ToString().Trim()));

            if (!string.IsNullOrEmpty(total.ToString()))
                predicate = predicate.Or(p => p.Total.ToString().Contains(total.ToString().Trim()));

            orden = orden.Where(predicate);
        }

        public async Task<bool> IsOrdenCompranNumberAsync(int OrdenNumer)
        {
            return await _ordencompra
                  .AllAsync(p => p.NumeroOrden != OrdenNumer);
        }

        //public async Task<bool> SeedDataAsync(int rowCount)
        //{
        //    foreach (OrdenCompra orden in _mockData.GetOrdenCompra(rowCount))
        //    {
        //        await this.AddAsync(orden);
        //    }
        //    return true;
        //}



    }
}
