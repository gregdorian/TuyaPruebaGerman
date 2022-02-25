using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Exceptions;
using TuyaPruebaGerman.Application.Interfaces.Repositories;
using TuyaPruebaGerman.Application.Wrappers;

namespace TuyaPruebaGerman.Application.Features.Productos.Commands.UpdateProductos
{
    public class UpdateProductoCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public class UpdatePositionCommandHandler : IRequestHandler<UpdateProductoCommand, Response<Guid>>
        {
            private readonly IProductoRepositoryAsync _productoRepository;

            public UpdatePositionCommandHandler(IProductoRepositoryAsync productoRepository)
            {
                _productoRepository = productoRepository;
            }

            public async Task<Response<Guid>> Handle(UpdateProductoCommand command, CancellationToken cancellationToken)
            {
                var producto = await _productoRepository.GetByIdAsync(command.Id);

                if (producto == null)
                {
                    throw new ApiException($"Producto Not Found.");
                }
                else
                {
                    producto.Descripcion = command.Description;
                    producto.Precio = command.Price;
                    await _productoRepository.UpdateAsync(producto);
                    return new Response<Guid>(producto.Id);
                }
            }
        }


    }
}
