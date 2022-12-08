using System;
using System.Collections.Generic;

namespace Employment.Data
{
    public partial class Address
    {
        public int Id { get; set; }
        public int StreetId { get; set; }
        public int HouseNumber { get; set; }
        public int? CorpusNumber { get; set; }
        public int? FlatNumber { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual Street Street { get; set; } = null!;
    }
}
