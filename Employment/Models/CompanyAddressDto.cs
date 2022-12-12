using System.Collections.Generic;
using Employment.Data;

namespace Employment.Models
{
    public class CompanyAddressDto
    {
        public string CompanyName { get; set; }
        public List<FullAddress>? FullAddresses { get; set; }
    }
}
