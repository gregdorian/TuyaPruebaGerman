using AutoMapper;
using TuyaPruebaGerman.Application.Features.Clientes.Queries;
using TuyaPruebaGerman.Application.Features.Logistica.Queries;
using TuyaPruebaGerman.Application.Features.Productos.Queries.GetProductos;
using TuyaPruebaGerman.Domain.Entities;

namespace TuyaPruebaGerman.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Cliente, GetClientesViewModel>().ReverseMap();
            CreateMap<Logistica, GetLogisticaViewModel>().ReverseMap();
            //CreateMap<CreateOrdenCompraCommand, OrdenCompra>();

            CreateMap<Producto, GetProductosViewModel>().ReverseMap();
            //CreateMap<CreateProductoCommand, Producto>();
        }
    }
}