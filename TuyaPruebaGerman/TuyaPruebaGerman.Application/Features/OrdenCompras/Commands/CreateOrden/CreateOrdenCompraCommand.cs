using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Interfaces.Repositories;
using TuyaPruebaGerman.Application.Wrappers;
using TuyaPruebaGerman.Domain.Entities;

namespace TuyaPruebaGerman.Application.Features.OrdenCompras.Commands.CreateOrden
{
    public class CreateOrdenCompraCommand : IRequest<Response<Guid>>
    {
        public string OrdenCompraNumber { get; set; }
        public string DireccionEnvio { get; set; }
        public bool Status { get; set; }
    }

    //public class CreateOrdenCompraHandler : IRequestHandler<CreateOrdenCompraCommand, Response<Guid>>
    //{
    //    private readonly IOrdenCompraRepositoryAsync _ordenCompraRepository;
    //    private readonly IMapper _mapper;
        
    //    public CreateOrdenCompraHandler(IOrdenCompraRepositoryAsync ordenCompraRepository, IMapper mapper)
    //    {
    //        _ordenCompraRepository = ordenCompraRepository;
    //        _mapper = mapper;
    //    }

    //    public async Task<Response<Guid>> Handle(CreateOrdenCompraCommand request, CancellationToken cancellationToken)
    //    {

    //        var ordenCompra = _mapper.Map<OrdenCompra>(request);
    //        await _ordenCompraRepository.AddAsync(ordenCompra);
    //        return new Response<Guid>(ordenCompra.Id);
    //    }
    //}
}
