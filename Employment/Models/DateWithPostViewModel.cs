using System;
using System.Collections.Generic;
using System.ComponentModel;
using Employment.Data;

namespace Employment.Models
{
    public class DateWithPostViewModel
    {
        public IEnumerable<Post> Posts { get; set; }

        [DisplayName("Выберите дату")]
        public DateTime Date { get; set; }

        [DisplayName("Введите название должности")]
        public string Post { get; set; }
    }
}
