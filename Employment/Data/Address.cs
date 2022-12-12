using System.ComponentModel;

namespace Employment.Data
{
    public partial class Address
    {
        public int Id { get; set; }

        [DisplayName("Улица")]
        public int StreetId { get; set; }

        [DisplayName("Номер дома")]
        public int HouseNumber { get; set; }

        [DisplayName("Номер корпуса")]
        public int? CorpusNumber { get; set; }

        [DisplayName("Номер квартиры/офиса")]
        public int? FlatNumber { get; set; }

        [DisplayName("Компания")]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual Street Street { get; set; } = null!;
    }
}
