using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TuyaPruebaGerman.Domain.Common;

namespace TuyaPruebaGerman.Domain.Entities
{
    public class OrdenCompra : AuditableBaseEntity
    {

        public int ProductoId { get; set; }

        public int ClienteId { get; set; }

        public int NumeroOrden { get; set; }

        public int Cantidad { get; set; }

        public decimal Total { get; set; }

        public List<Producto> Productos { get; set; }

        public List<Cliente> Clientes { get; set; }

        //public void AddItem(ItemOrden newItem)
        //{
        //    Guard.Against.Null(newItem, nameof(newItem));
        //    _items.Add(newItem);

        //    var newItemAddedEvent = new NewItemAddedEvent(this, newItem);
        //    Events.Add(newItemAddedEvent);
        //}
    }
}
