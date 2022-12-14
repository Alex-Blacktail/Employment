using System.Linq;
using System.Text;
using Employment.Data;

namespace Employment.Models
{
    public class CompanyDto
    {
        public Company Company;
        public string? FullAddress;

        public string GetPhoneNumbers()
        {
            var sb = new StringBuilder(8);

            var phones = Company.PhoneNumbers.ToArray();

            if(phones.Length > 1)
            {
                for (int i = 0; i < phones.Length - 1; i++)
                    sb.Append(phones[i].PhoneNumber1).Append(", ");

                sb.Append(phones[phones.Length - 1].PhoneNumber1);
            }
            else if(phones.Length > 0)
            {
                sb.Append(phones[0].PhoneNumber1);
            }
            else
            {
                sb.Append("-");
            }
            return sb.ToString();
        }
    }
}
