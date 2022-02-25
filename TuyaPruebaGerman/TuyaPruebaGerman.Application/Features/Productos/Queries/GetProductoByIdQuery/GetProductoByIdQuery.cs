

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Exceptions;
using TuyaPruebaGerman.Application.Interfaces.Repositories;
using TuyaPruebaGerman.Application.Wrappers;
using TuyaPruebaGerman.Domain.Entities;

namespace TuyaPruebaGerman.Application.Features.Productos.Queries.GetProductoByIdQuery
{
    public class GetProductoByIdQuery : IRequest<Response<Producto>>
    {
        public Guid Id { get; set; }


        public class GetProductoByIdQueryHandler : IRequestHandler<GetProductoByIdQuery, Response<Producto>>
        {

            private readonly IProductoRepositoryAsync _productoRepository;
            

            public GetProductoByIdQueryHandler(IProductoRepositoryAsync productoRepository)
            {
                _productoRepository = productoRepository;
            }

            public async Task<Response<Producto>> Handle(GetProductoByIdQuery query, CancellationToken cancellationToken)
            {
                var producto = await _productoRepository.GetByIdAsync(query.Id);
                if (producto == null) throw new ApiException($"Producto No Encontrado.");
                return new Response<Producto>(producto);
            }
        }
    }
}
