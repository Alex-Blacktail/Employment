using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Employment.Data
{
    public partial class StreetType
    {
        public StreetType()
        {
            Streets = new HashSet<Street>();
        }

        public int Id { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; } = null!;

        [DisplayName("Краткое наименование")]
        public string ShortName { get; set; } = null!;

        public virtual ICollection<Street> Streets { get; set; }
    }
}
