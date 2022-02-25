using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Interfaces;
using TuyaPruebaGerman.Application.Interfaces.Repositories;
using TuyaPruebaGerman.Application.Parameters;
using TuyaPruebaGerman.Application.Wrappers;
using TuyaPruebaGerman.Domain.Entities;

namespace TuyaPruebaGerman.Application.Features.Productos.Queries.GetProductos
{
    public class GetProductosQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public decimal Precio { get; set; }

        public string Descripcion { get; set; }
    }
    public class GetAllProductosQueryHandler : IRequestHandler<GetProductosQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IProductoRepositoryAsync _productoRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;

        public GetAllProductosQueryHandler(IProductoRepositoryAsync productoRepository, IMapper mapper, IModelHelper modelHelper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;
        }

        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetProductosQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetProductosViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetProductosViewModel>();
            }
            // query based on filter
            var entityProductos = await _productoRepository.GetPagedProductoReponseAsync(validFilter);
            var data = entityProductos.data;
            RecordsCount recordCount = entityProductos.recordsCount;

            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }

    }

}
