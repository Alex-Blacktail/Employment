using System;
using System.Collections.Generic;

namespace Employment.Data
{
    public partial class Requirment
    {
        public int Id { get; set; }
        public int LowerAgeLimit { get; set; }
        public int UpperAgeLimit { get; set; }
        public string CommunicationSkill { get; set; } = null!;
        public int PostId { get; set; }
        public int GenderId { get; set; }

        public virtual Gender Gender { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
