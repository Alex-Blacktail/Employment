using System;
using System.Collections.Generic;

namespace Employment.Data
{
    public partial class SocialResponsibility
    {
        public int Id { get; set; }
        public string EmploymentBook { get; set; } = null!;
        public string SocialPackage { get; set; } = null!;
        public int PostId { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
