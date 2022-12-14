using System;
using System.Collections.Generic;
using System.ComponentModel;
using Employment.Data;

namespace Employment.Models
{
    public class PeriodMaxPostsViewModel
    {
        public IEnumerable<Post> Posts { get; set; }

        [DisplayName("Выберите дату начала")]
        public DateTime BeginDate { get; set; }

        [DisplayName("Выберите дату конца")]
        public DateTime EndDate { get; set; }
    }
}
