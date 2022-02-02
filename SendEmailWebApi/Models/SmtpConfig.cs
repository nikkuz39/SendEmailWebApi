using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendEmailWebApi.Models
{
    /// <summary>
    /// Данный класс содержит свойства для протокола SMTP
    /// </summary>
    public class SmtpConfig
    {
        /// <summary>
        /// Свойство хранит адрес получателя 
        /// </summary>
        public string FromAddress { get; set; }
        /// <summary>
        /// Свойство хранит имя пользователя
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Свойство хранит пароль
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Свойство хранит имя хоста
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Свойство хранит номер порта
        /// </summary>
        public int Port { get; set; }
    }
}
