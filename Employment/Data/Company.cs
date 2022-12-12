using System;
using System.Collections.Generic;
using System.ComponentModel;

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

        [DisplayName("Наименование")]
        public string Name { get; set; } = null!;

        [DisplayName("Краткое наименование")]
        public string? ShortName { get; set; }

        [DisplayName("Почтовый адрес")]
        public string? Email { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
