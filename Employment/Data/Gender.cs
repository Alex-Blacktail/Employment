using System.Collections.Generic;
using System.ComponentModel;

namespace Employment.Data
{
    public partial class Gender
    {
        public Gender()
        {
            Requirments = new HashSet<Requirment>();
        }

        public int Id { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Requirment> Requirments { get; set; }
    }
}
