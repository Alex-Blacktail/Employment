using System;
using System.Collections.Generic;

namespace Employment.Data
{
    public partial class LocalityType
    {
        public LocalityType()
        {
            Localities = new HashSet<Locality>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ShortName { get; set; } = null!;

        public virtual ICollection<Locality> Localities { get; set; }
    }
}
