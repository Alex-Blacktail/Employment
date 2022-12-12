using System.ComponentModel;

namespace Employment.Data
{
    public partial class PhoneNumber
    {
        public int Id { get; set; }

        [DisplayName("Номер")]
        public string PhoneNumber1 { get; set; } = null!;

        [DisplayName("Компания")]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; } = null!;
    }
}
