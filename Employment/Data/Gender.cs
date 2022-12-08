using System;
using System.Collections.Generic;

namespace Employment.Data
{
    public partial class Gender
    {
        public Gender()
        {
            Requirments = new HashSet<Requirment>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Requirment> Requirments { get; set; }
    }
}
