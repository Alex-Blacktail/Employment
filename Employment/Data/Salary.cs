using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Employment.Data
{
    public partial class Salary
    {
        public int Id { get; set; }

        [DisplayName("Нижняя граница")]
        public decimal LowerLimit { get; set; }

        [DisplayName("Верхняя граница")]
        public decimal UpperLimit { get; set; }

        [DisplayName("Должность")]
        public int PostId { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
