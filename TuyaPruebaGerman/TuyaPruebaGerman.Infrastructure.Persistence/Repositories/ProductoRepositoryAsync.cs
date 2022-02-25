using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Features.Productos.Queries.GetProductos;
using TuyaPruebaGerman.Application.Interfaces;
using TuyaPruebaGerman.Application.Interfaces.Repositories;
using TuyaPruebaGerman.Application.Parameters;
using TuyaPruebaGerman.Domain.Entities;
using TuyaPruebaGerman.Infrastructure.Persistence.Contexts;
using TuyaPruebaGerman.Infrastructure.Persistence.Repository;

namespace TuyaPruebaGerman.Infrastructure.Persistence.Repositories
{
    public class ProductoRepositoryAsync : GenericRepositoryAsync<Producto>, IProductoRepositoryAsync
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Producto> _producto;
        private readonly IDataShapeHelper<Producto> _dataShaper;
        private readonly IMockService _mockData;

        public ProductoRepositoryAsync(
            ApplicationDbContext dbContext,
            IDataShapeHelper<Producto> dataShaper,
            IMockService mockData) : base(dbContext)
        {
            _dbContext = dbContext;
            _producto = dbContext.Set<Producto>();
            _dataShaper = dataShaper;
            _mockData = mockData;
        }

        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedProductoReponseAsync(GetProductosQuery requestParameter)
        {
            var descripcion = requestParameter.Descripcion;
            var precio = requestParameter.Precio;

            var pageNumber = requestParameter.PageNumber;
            var pageSize = requestParameter.PageSize;
            var orderBy = requestParameter.OrderBy;
            var fields = requestParameter.Fields;

            int recordsTotal, recordsFiltered;

            // Setup IQueryable
            var result = _producto
                .AsNoTracking()
                .AsExpandable();

            // Count records total
            recordsTotal = await result.CountAsync();

            // filter data
            FilterByColumn(ref result, descripcion, precio);

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
                result = result.Select<Producto>("new(" + fields + ")");
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

        private void FilterByColumn(ref IQueryable<Producto> result, string descripcion, decimal precio)
        {

            if (string.IsNullOrEmpty(descripcion) && string.IsNullOrEmpty(precio.ToString()))
                return;

            var predicate = PredicateBuilder.New<Producto>();

            if (!string.IsNullOrEmpty(descripcion))
                predicate = predicate.Or(p => p.Descripcion.Contains(descripcion.Trim()));

            if (!string.IsNullOrEmpty(precio.ToString()))// or not zero!!!
                predicate = predicate.Or(p => p.Precio.Equals(precio));

            result = result.Where(predicate);
        }

        public async Task<bool> IsProductoDescriptionAsync(string descripcion)
        {
            return await _producto
                  .AllAsync(p => p.Descripcion != descripcion);
        }

        public async Task<bool> SeedDataAsync(int rowCount)
        {
            foreach (Producto producto in _mockData.GetProductos(rowCount))
            {
                await this.AddAsync(producto);
            }
            return true;
        }
    }
}
