using System;
using System.Collections.Generic;

namespace Employment.Data
{
    public partial class PhoneNumber
    {
        public int Id { get; set; }
        public string PhoneNumber1 { get; set; } = null!;
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; } = null!;
    }
}
