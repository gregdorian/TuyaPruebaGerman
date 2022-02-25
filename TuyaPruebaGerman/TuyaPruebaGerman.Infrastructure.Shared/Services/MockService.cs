using System.Collections.Generic;
using TuyaPruebaGerman.Application.Interfaces;
using TuyaPruebaGerman.Domain.Entities;
using TuyaPruebaGerman.Infrastructure.Shared.Mock;

namespace TuyaPruebaGerman.Infrastructure.Shared.Services
{
    public class MockService : IMockService
    {
        public List<Cliente> GetClientes(int rowCount)
        {
            var ClienteFaker = new ClienteSeedBogusConfig();
            return ClienteFaker.Generate(rowCount);
        }

        public List<Producto> GetProductos(int rowCount)
        {
            var productoFaker = new ProductoSeedBogusConfig();
            return productoFaker.Generate(rowCount);
        }

        public List<Cliente> SeedCliente(int rowCount)
        {
            var seedClienteFaker = new ClienteSeedBogusConfig();
            return seedClienteFaker.Generate(rowCount);
        }

        public List<Producto> SeedProducto(int rowCount)
        {
            var seedProductoFaker = new ProductoSeedBogusConfig();
            return seedProductoFaker.Generate(rowCount);
        }
    }
}