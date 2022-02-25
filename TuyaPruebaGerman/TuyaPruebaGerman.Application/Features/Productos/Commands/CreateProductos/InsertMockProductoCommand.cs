using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Interfaces.Repositories;
using TuyaPruebaGerman.Application.Wrappers;

namespace TuyaPruebaGerman.Application.Features.Productos.Commands.CreateProductos
{
    public partial class InsertMockProductoCommand : IRequest<Response<int>>
    {
        public int RowCount { get; set; }
    }

    public class SeedProductoCommandHandler : IRequestHandler<InsertMockProductoCommand, Response<int>>
    {
        private readonly IProductoRepositoryAsync _productoRepository;

        public SeedProductoCommandHandler(IProductoRepositoryAsync productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<Response<int>> Handle(InsertMockProductoCommand request, CancellationToken cancellationToken)
        {
             await _productoRepository.SeedDataAsync(request.RowCount);
            return new Response<int>(request.RowCount);
        }

    }
}
