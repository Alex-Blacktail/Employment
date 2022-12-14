using System;
using System.Collections.Generic;
using System.ComponentModel;
using Employment.Data;

namespace Employment.Models
{
    public class DateWithoutEduViewModel
    {
        public IEnumerable<Post> Posts { get; set; }

        [DisplayName("Выберите дату")]
        public DateTime Date { get; set; }
    }
}
