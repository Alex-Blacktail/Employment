using System.ComponentModel;

namespace Employment.Data
{
    public partial class Responsibility
    {
        public int Id { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; } = null!;

        [DisplayName("Должность")]
        public int PostId { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
