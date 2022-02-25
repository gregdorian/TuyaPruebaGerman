using AutoBogus;
using Bogus;
using System;
using TuyaPruebaGerman.Domain.Entities;

namespace TuyaPruebaGerman.Infrastructure.Shared.Mock
{
    public class ClienteSeedBogusConfig : AutoFaker<Cliente>
    {

        public ClienteSeedBogusConfig() 
        {
            Randomizer.Seed = new Random(8675309);
            var id = 1;
            RuleFor(m => m.Id, f => Guid.NewGuid());

            RuleFor(o => o.Nombres, f => f.Name.FirstName());
            RuleFor(o => o.Apellidos, f => f.Name.LastName());
            RuleFor(o => o.Telefono, f => f.Phone.PhoneNumber());
            RuleFor(o => o.Direccion, f => f.Address.FullAddress());
            RuleFor(o => o.Ciudad, f => f.Address.City());
            //For Auditory Base Entity
            RuleFor(o => o.Created, f => f.Date.Past(1));
            RuleFor(o => o.CreatedBy, f => f.Name.FullName());
            RuleFor(o => o.LastModified, f => f.Date.Recent(1));
            RuleFor(o => o.LastModifiedBy, f => f.Name.FullName());
        }

    }
}
