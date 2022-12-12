using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Employment.Data
{
    public partial class Locality
    {
        public Locality()
        {
            Streets = new HashSet<Street>();
        }

        public int Id { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; } = null!;

        [DisplayName("Тип НП")]
        public int LocalityTypeId { get; set; }

        public virtual LocalityType LocalityType { get; set; } = null!;
        public virtual ICollection<Street> Streets { get; set; }
    }
}
