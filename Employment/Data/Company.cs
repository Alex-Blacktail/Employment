using System;
using System.Collections.Generic;

namespace Employment.Data
{
    public partial class Company
    {
        public Company()
        {
            Addresses = new HashSet<Address>();
            PhoneNumbers = new HashSet<PhoneNumber>();
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ShortName { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
