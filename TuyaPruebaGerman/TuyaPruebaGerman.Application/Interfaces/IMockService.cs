using System.Collections.Generic;
using TuyaPruebaGerman.Domain.Entities;

namespace TuyaPruebaGerman.Application.Interfaces
{
    public interface IMockService
    {
        List<Cliente> GetClientes(int rowCount);

        List<Producto> GetProductos(int rowCount);

        List<Cliente> SeedCliente(int rowCount);

        List<Producto> SeedProducto(int rowCount);
    }
}