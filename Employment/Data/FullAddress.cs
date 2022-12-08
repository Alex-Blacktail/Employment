using System;
using System.Collections.Generic;

namespace Employment.Data
{
    public partial class FullAddress
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string ShortLocalityType { get; set; } = null!;
        public string LocalityName { get; set; } = null!;
        public string ShortStreetType { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public int HouseNumber { get; set; }
        public int? CorpusNumber { get; set; }
        public int? FlatNumber { get; set; }
    }
}
