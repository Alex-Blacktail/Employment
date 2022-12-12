using System;
using System.Collections.Generic;
using System.ComponentModel;

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

        [DisplayName("Наименование")]
        public string Name { get; set; } = null!;

        [DisplayName("Кртакое наименование")]
        public string? ShortName { get; set; }

        [DisplayName("Дата открытия вакансии")]
        public DateTime BeginDate { get; set; }

        [DisplayName("Дата закрытия вакансии")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Компания")]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<Requirment> Requirments { get; set; }
        public virtual ICollection<Responsibility> Responsibilities { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<SocialResponsibility> SocialResponsibilities { get; set; }
    }
}
