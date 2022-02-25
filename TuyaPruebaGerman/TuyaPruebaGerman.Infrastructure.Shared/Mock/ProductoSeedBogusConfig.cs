using AutoBogus;
using Bogus;
using System;
using TuyaPruebaGerman.Domain.Entities;

namespace TuyaPruebaGerman.Infrastructure.Shared.Mock
{
    public class ProductoSeedBogusConfig : AutoFaker<Producto>
    {
        public ProductoSeedBogusConfig()
        {
            Randomizer.Seed = new Random(8675309);
            var id = 1;
            RuleFor(m => m.Id, f => Guid.NewGuid());

            RuleFor(o => o.Descripcion, f => f.Commerce.ProductName());
            RuleFor(o => o.Precio, f => decimal.Parse(f.Commerce.Price()));

            //For Auditory Base Entity
            RuleFor(o => o.Created, f => f.Date.Past(1));
            RuleFor(o => o.CreatedBy, f => f.Name.FullName());
            RuleFor(o => o.LastModified, f => f.Date.Recent(1));
            RuleFor(o => o.LastModifiedBy, f => f.Name.FullName());
        }
    }
}
