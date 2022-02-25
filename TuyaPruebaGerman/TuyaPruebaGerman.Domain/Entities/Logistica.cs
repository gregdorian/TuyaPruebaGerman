using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TuyaPruebaGerman.Domain.Common;

namespace TuyaPruebaGerman.Domain.Entities
{
    public class Logistica : AuditableBaseEntity
    {

        public int OrdenCompraId { get; set; }

        public string NombreCompania { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public List<OrdenCompra> OrdenesCompra { get; set; }


    }
}
