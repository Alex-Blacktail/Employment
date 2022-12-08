using System;
using System.Collections.Generic;

namespace Employment.Data
{
    public partial class Salary
    {
        public int Id { get; set; }
        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
