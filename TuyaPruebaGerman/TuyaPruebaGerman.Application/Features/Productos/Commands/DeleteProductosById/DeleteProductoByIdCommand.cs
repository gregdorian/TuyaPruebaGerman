using TuyaPruebaGerman.Application.Exceptions;
using TuyaPruebaGerman.Application.Interfaces.Repositories;
using TuyaPruebaGerman.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TuyaPruebaGerman.Application.Features.Productos.Commands.DeleteProductosById
{
    public class DeleteProductoByIdCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }

        public class DeleteProductoByIdCommandHandler : IRequestHandler<DeleteProductoByIdCommand, Response<Guid>>
        {
            private readonly IProductoRepositoryAsync _productoRepository;


            public DeleteProductoByIdCommandHandler(IProductoRepositoryAsync productoRepository)
            {
                _productoRepository = productoRepository;
            }
            public async Task<Response<Guid>> Handle(DeleteProductoByIdCommand command, CancellationToken cancellationToken)
            {
                var Producto = await _productoRepository.GetByIdAsync(command.Id);
                if (Producto == null) throw new ApiException($"Producto Not Found.");
                await _productoRepository.DeleteAsync(Producto);
                return new Response<Guid>(Producto.Id);
            }
        }
    }
}
