using Employment.Data;

namespace Employment.Models
{
    public class PostViewModel
    {
        /// <summary>
        /// Информация о должности
        /// </summary>
        public Post Post { get; set; }

        /// <summary>
        /// Зарплата
        /// </summary>
        public Salary Salary { get; set; }

        /// <summary>
        /// Требования
        /// </summary>
        public Requirment Requirment { get; set; }

        /// <summary>
        /// Соц обязательства
        /// </summary>
        public SocialResponsibility SocialResponsibility { get; set; }
    }
}
