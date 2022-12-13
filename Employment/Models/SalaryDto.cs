using System.ComponentModel;

namespace Employment.Models
{
    public class SalaryDto
    {
        public int Id { get; set; }
        public int PostId { get; set; }

        [DisplayName("Нижняя граница")]
        public string LowerLimit { get; set; }

        [DisplayName("Верхняя граница")]
        public string UpperLimit { get; set; }
    }
}
