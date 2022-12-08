using System;
using System.Collections.Generic;

namespace Employment.Data
{
    public partial class Street
    {
        public Street()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int StreetTypeId { get; set; }
        public int LocalityId { get; set; }

        public virtual Locality Locality { get; set; } = null!;
        public virtual StreetType StreetType { get; set; } = null!;
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
