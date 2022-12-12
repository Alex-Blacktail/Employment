using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Employment.Data
{
    public partial class Skill
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; } = null!;

        [DisplayName("Уровень")]
        public string Level { get; set; } = null!;

        [DisplayName("Должность")]
        public int PostId { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
