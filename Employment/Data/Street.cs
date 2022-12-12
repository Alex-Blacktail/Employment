using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Employment.Data
{
    public partial class Street
    {
        public Street()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; } = null!;

        [DisplayName("Тип улицы")]
        public int StreetTypeId { get; set; }

        [DisplayName("Населенный пункт")]
        public int LocalityId { get; set; }

        public virtual Locality Locality { get; set; } = null!;
        public virtual StreetType StreetType { get; set; } = null!;
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
