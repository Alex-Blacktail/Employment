using System;
using System.Collections.Generic;

namespace Employment.Data
{
    public partial class StreetType
    {
        public StreetType()
        {
            Streets = new HashSet<Street>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ShortName { get; set; } = null!;

        public virtual ICollection<Street> Streets { get; set; }
    }
}
