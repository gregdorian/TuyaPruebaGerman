using System.ComponentModel.DataAnnotations;
using TuyaPruebaGerman.Domain.Common;

namespace TuyaPruebaGerman.Domain.Entities
{
    public class Cliente : AuditableBaseEntity
    {

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0}, {1}", Apellidos, this.Nombres);
            }
            set { }

        }
        public string Telefono { get; set; }

        public string Direccion{ get; set; }

        public string Ciudad { get; set; }
    }
}
