using System.ComponentModel.DataAnnotations;
using TuyaPruebaGerman.Domain.Common;


namespace TuyaPruebaGerman.Domain.Entities
{
    public class Producto : AuditableBaseEntity
    {

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }
    }
}
