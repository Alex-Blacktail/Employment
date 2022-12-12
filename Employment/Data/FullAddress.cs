using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Employment.Data
{
    public partial class FullAddress
    {
        public int Id { get; set; }

        [DisplayName("")]
        public int CompanyId { get; set; }

        [DisplayName("")]
        public string ShortLocalityType { get; set; } = null!;

        [DisplayName("")]
        public string LocalityName { get; set; } = null!;

        [DisplayName("")]
        public string ShortStreetType { get; set; } = null!;

        [DisplayName("")]
        public string StreetName { get; set; } = null!;

        [DisplayName("")]
        public int HouseNumber { get; set; }

        [DisplayName("")]
        public int? CorpusNumber { get; set; }

        [DisplayName("")]
        public int? FlatNumber { get; set; }
    }
}
