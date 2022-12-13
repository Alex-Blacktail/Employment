namespace Employment.Models
{
    public class SalaryDto
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string LowerLimit { get; set; }
        public string UpperLimit { get; set; }
    }
}
