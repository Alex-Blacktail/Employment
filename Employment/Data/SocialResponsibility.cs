using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Employment.Data
{
    public partial class SocialResponsibility
    {
        public int Id { get; set; }

        [DisplayName("Трудовая книга")]
        public string EmploymentBook { get; set; } = null!;

        [DisplayName("Социальный пакет")]
        public string SocialPackage { get; set; } = null!;

        [DisplayName("Должность")]
        public int PostId { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
