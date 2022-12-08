using System;
using System.Collections.Generic;

namespace Employment.Data
{
    public partial class Post
    {
        public Post()
        {
            Requirments = new HashSet<Requirment>();
            Responsibilities = new HashSet<Responsibility>();
            Salaries = new HashSet<Salary>();
            Skills = new HashSet<Skill>();
            SocialResponsibilities = new HashSet<SocialResponsibility>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ShortName { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<Requirment> Requirments { get; set; }
        public virtual ICollection<Responsibility> Responsibilities { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<SocialResponsibility> SocialResponsibilities { get; set; }
    }
}
