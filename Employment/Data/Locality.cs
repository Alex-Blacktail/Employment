using System;
using System.Collections.Generic;

namespace Employment.Data
{
    public partial class Locality
    {
        public Locality()
        {
            Streets = new HashSet<Street>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int LocalityTypeId { get; set; }

        public virtual LocalityType LocalityType { get; set; } = null!;
        public virtual ICollection<Street> Streets { get; set; }
    }
}
