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

namespace TuyaPruebaGerman.Application.Features.Clientes.Queries
{
    public class GetClientesQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public string ClienteNumber { get; set; }

        public string ClienteName { get; set; }

    }

    public class GetAllClientesQueryHandler : IRequestHandler<GetClientesQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IClienteRepositoryAsync _clienteRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;

        public GetAllClientesQueryHandler(IClienteRepositoryAsync clienteRepository, IMapper mapper, IModelHelper modelHelper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;

        }

        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetClientesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetClientesViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetClientesViewModel>();
            }
            // query based on filter
            var entityClientes = await _clienteRepository.GetPagedClienteReponseAsync(validFilter);
            var data = entityClientes.data;
            RecordsCount recordCount = entityClientes.recordsCount;

            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }



    }

}
