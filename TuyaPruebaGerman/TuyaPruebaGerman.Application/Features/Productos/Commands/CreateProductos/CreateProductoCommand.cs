using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Interfaces.Repositories;
using TuyaPruebaGerman.Application.Wrappers;
using TuyaPruebaGerman.Domain.Entities;

namespace TuyaPruebaGerman.Application.Features.Productos.Commands.CreateProductos
{
    public class CreateProductoCommand : IRequest<Response<Guid>>
    {

        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }

    public class CreateProductoHandler : IRequestHandler<CreateProductoCommand, Response<Guid>>
    {
        private readonly IProductoRepositoryAsync _productoRepository;
        private readonly IMapper _mapper;

        public CreateProductoHandler(IProductoRepositoryAsync productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {

            var producto = _mapper.Map<Producto>(request);
            await _productoRepository.AddAsync(producto);
            return new Response<Guid>(producto.Id);
        }
    }
}
