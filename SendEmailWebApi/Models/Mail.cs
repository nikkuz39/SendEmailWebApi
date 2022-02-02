using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SendEmailWebApi.Models
{
    /// <summary>
    /// Данный класс содержит свойства, определяющие форму объекта - письмо
    /// </summary>
    public class Mail
    {
        /// <summary>
        /// Свойство хранит первичный ключ
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Свойство хранит тему письма
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Свойство хранит текс письма
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Коллекция для хранения адресов получателей
        /// </summary>
        [NotMapped]
        public ICollection<string> RecipientsList { get; set; }
        /// <summary>
        /// Свойство хранит адреса получателей
        /// </summary>
        public string Recipients 
        { 
            get { return string.Join(",", RecipientsList); }
            set { RecipientsList = value.Split(",").ToList(); } 
        }
        /// <summary>
        /// Свойство хранит дату и время
        /// </summary>
        public string DateAndTime { get; set; }
        /// <summary>
        /// Свойство хранит результат отправки письма(отправлено, либо нет)
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// Свойство хранит исключения возникающие при отправки письма
        /// </summary>
        public string FailedMessage { get; set; }
    }
}
