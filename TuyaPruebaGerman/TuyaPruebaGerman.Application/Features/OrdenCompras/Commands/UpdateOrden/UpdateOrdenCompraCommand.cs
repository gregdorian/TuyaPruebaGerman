using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Exceptions;
using TuyaPruebaGerman.Application.Interfaces.Repositories;
using TuyaPruebaGerman.Application.Wrappers;

namespace TuyaPruebaGerman.Application.Features.OrdenCompras.Commands.UpdateOrden
{
    public class UpdateOrdenCompraCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public class UpdateOrdenCompraCommandHandler : IRequestHandler<UpdateOrdenCompraCommand, Response<Guid>>
        {
            private readonly IOrdenCompraRepositoryAsync _ordenCompraRepository;

            public UpdateOrdenCompraCommandHandler(IOrdenCompraRepositoryAsync ordenCompraRepository)
            {
                _ordenCompraRepository = ordenCompraRepository;
            }

            public async Task<Response<Guid>> Handle(UpdateOrdenCompraCommand command, CancellationToken cancellationToken)
            {
                var ordenCompra = await _ordenCompraRepository.GetByIdAsync(command.Id);

                if (ordenCompra == null)
                {
                    throw new ApiException($"Position Not Found.");
                }
                else
                {
                    ordenCompra.Cantidad = command.Cantidad;
                    ordenCompra.Total = command.Total;
                    await _ordenCompraRepository.UpdateAsync(ordenCompra);
                    return new Response<Guid>(ordenCompra.Id);
                }
            }
        }

    }
}
