using System;
using System.Collections.Generic;

namespace Employment.Data
{
    public partial class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Level { get; set; } = null!;
        public int PostId { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
