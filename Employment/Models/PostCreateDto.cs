using System.ComponentModel;
using System;

namespace Employment.Models
{
    public class PostCreateDto
    {
        //public int Id { get; set; }

        //Основное

        [DisplayName("Наименование")]
        public string Name { get; set; } = null!;

        [DisplayName("Кртакое наименование")]
        public string? ShortName { get; set; }

        [DisplayName("Дата открытия вакансии")]
        public DateTime BeginDate { get; set; }

        [DisplayName("Дата закрытия вакансии")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Компания")]
        public int CompanyId { get; set; }

        //Зарплата

        /// <summary>
        /// Нижняя граница зарплаты
        /// </summary>
        [DisplayName("Нижняя граница")]
        public decimal LowerLimit { get; set; }

        /// <summary>
        /// Верхняя граница зарплаты
        /// </summary>
        [DisplayName("Верхняя граница")]
        public decimal UpperLimit { get; set; }

        //Соц обязательства

        [DisplayName("Трудовая книга")]
        public string EmploymentBook { get; set; } = null!;

        [DisplayName("Социальный пакет")]
        public string SocialPackage { get; set; } = null!;

        //Требования

        [DisplayName("Нижний порог возраста")]
        public int LowerAgeLimit { get; set; }

        [DisplayName("Верхний порог возраста")]
        public int UpperAgeLimit { get; set; }

        [DisplayName("Навыки общения")]
        public string CommunicationSkill { get; set; } = null!;

        [DisplayName("Пол")]
        public int GenderId { get; set; }
    }
}
