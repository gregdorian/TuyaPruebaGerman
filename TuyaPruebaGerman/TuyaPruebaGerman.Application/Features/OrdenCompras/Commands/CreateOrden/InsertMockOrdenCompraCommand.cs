using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Interfaces.Repositories;
using TuyaPruebaGerman.Application.Wrappers;

namespace TuyaPruebaGerman.Application.Features.OrdenCompras.Commands.CreateOrden
{
    public partial class InsertMockOrdenCompraCommand : IRequest<Response<int>>
    {
        public int RowCount { get; set; }
    }

    //public class SeedOrdenCompraCommandHandler : IRequestHandler<InsertMockOrdenCompraCommand, Response<int>>
    //{
    //    private readonly IOrdenCompraRepositoryAsync _ordenCompraRepository;

    //    public SeedOrdenCompraCommandHandler(IOrdenCompraRepositoryAsync ordenCompraRepository)
    //    {
    //        _ordenCompraRepository = ordenCompraRepository;
    //    }

    //    //public async Task<Response<int>> Handle(InsertMockOrdenCompraCommand request, CancellationToken cancellationToken)
    //    //{
    //    //    await _ordenCompraRepository.SeedDataAsync(request.RowCount);
    //    //    return new Response<int>(request.RowCount);
    //    //}
    //}
}
