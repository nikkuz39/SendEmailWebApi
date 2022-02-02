using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SendEmailWebApi.Models;

namespace SendEmailWebApi.Models
{
    /// <summary>
    /// Для взаимодействия с базой данных, используем класс DbContext
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// DbSet представляет набор объектов, которые хранятся в базе данных и взаимодействует с классом
        /// </summary>
        public DbSet<Mail> Mails { get; set; }

        /// <summary>
        /// Устанавливаем параметры подключения к базе данных
        /// </summary>
        /// <param name="options">Строка подключения к базе данных</param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\Data\postaldata.db");
    }
}
