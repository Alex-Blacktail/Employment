using System;
using System.Collections.Generic;

namespace Employment.Data
{
    public partial class Responsibility
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int PostId { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
