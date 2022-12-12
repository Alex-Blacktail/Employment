using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Employment.Data
{
    public partial class FullAddress
    {
        public int Id { get; set; }

        [DisplayName("Компания")]
        public int CompanyId { get; set; }

        [DisplayName("Тип НП")]
        public string ShortLocalityType { get; set; } = null!;

        [DisplayName("Населенный пункт")]
        public string LocalityName { get; set; } = null!;

        [DisplayName("Тип улицы")]
        public string ShortStreetType { get; set; } = null!;

        [DisplayName("Улица")]
        public string StreetName { get; set; } = null!;

        [DisplayName("Дом")]
        public int HouseNumber { get; set; }

        [DisplayName("Корпус")]
        public int? CorpusNumber { get; set; }

        [DisplayName("Квартира")]
        public int? FlatNumber { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder(16);

            sb.Append($"{ShortLocalityType}. {LocalityName}, ");
            sb.Append($"{ShortStreetType}. {StreetName}, ");
            sb.Append($"дом {HouseNumber}");

            if(CorpusNumber != null)
                sb.Append($", корп. {CorpusNumber}");

            if (FlatNumber != null)
                sb.Append($", кв. {FlatNumber}");

            return sb.ToString();
        }
    }
}
