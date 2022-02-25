using MediatR;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Interfaces;
using TuyaPruebaGerman.Application.Interfaces.Repositories;
using TuyaPruebaGerman.Application.Parameters;
using TuyaPruebaGerman.Application.Wrappers;
using TuyaPruebaGerman.Domain.Entities;
using TuyaPruebaGerman.Application.Features.OrdenCompras.Queries;

namespace TuyaPruebaGerman.Application.Features.OrdenCompra.Queries
{
    public class GetOrdenCompraQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public int NumberOrdenCompra { get; set; }

        public decimal Total { get; set; }
    }

    public class GetOrdenCompraQueryHandler : IRequestHandler<GetOrdenCompraQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IOrdenCompraRepositoryAsync _ordenCompraRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;

        public GetOrdenCompraQueryHandler(
            IOrdenCompraRepositoryAsync ordenCompraRepository,
            IMapper mapper,
            IModelHelper modelHelper
            )
        {
            _ordenCompraRepository = ordenCompraRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;

        }

        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetOrdenCompraQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;

            if (!string.IsNullOrEmpty(validFilter.Fields))
            {

                validFilter.Fields = _modelHelper.ValidateModelFields<GetOrdenCompraViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {

                validFilter.Fields = _modelHelper.GetModelFields<GetOrdenCompraViewModel>();
            }
            // query based on filter
            var entitylogistica = await _ordenCompraRepository.GetPagedOrdenCompraReponseAsync(validFilter);
            var data = entitylogistica.data;
            RecordsCount recordCount = entitylogistica.recordsCount;

            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }

}
