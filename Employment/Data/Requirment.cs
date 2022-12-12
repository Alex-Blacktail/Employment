using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Employment.Data
{
    public partial class Requirment
    {
        public int Id { get; set; }

        [DisplayName("Нижний порог возраста")]
        public int LowerAgeLimit { get; set; }

        [DisplayName("Верхний порог возраста")]
        public int UpperAgeLimit { get; set; }

        [DisplayName("Навыки общения")]
        public string CommunicationSkill { get; set; } = null!;

        [DisplayName("Должность")]
        public int PostId { get; set; }

        [DisplayName("Пол")]
        public int GenderId { get; set; }

        public virtual Gender Gender { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
