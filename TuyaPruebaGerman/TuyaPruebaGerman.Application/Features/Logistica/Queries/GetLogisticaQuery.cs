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

namespace TuyaPruebaGerman.Application.Features.Logistica.Queries
{
    public class GetLogisticaQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public string LogisticaNumber { get; set; }

        public string LogisticaName { get; set; }
    }

    public class GetAllLogisticaQueryHandler : IRequestHandler<GetLogisticaQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly ILogisticaRepositoryAsync _logisticaRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;

        public GetAllLogisticaQueryHandler(
            ILogisticaRepositoryAsync logisticaRepository,
            IMapper mapper,
            IModelHelper modelHelper)
        {
            _logisticaRepository = logisticaRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;
        }

        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetLogisticaQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;
           
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                
                validFilter.Fields = _modelHelper.ValidateModelFields<GetLogisticaViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                
                validFilter.Fields = _modelHelper.GetModelFields<GetLogisticaViewModel>();
            }
            // query based on filter
            var entitylogistica = await _logisticaRepository.GetPagedLogisticaReponseAsync(validFilter);
            var data = entitylogistica.data;
            RecordsCount recordCount = entitylogistica.recordsCount;

            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }


    }

}
