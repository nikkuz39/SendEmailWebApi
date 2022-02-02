using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SendEmailWebApi.Models;
using Microsoft.Extensions.Options;

namespace SendEmailWebApi.Controllers
{
    /// <summary>
    /// Контроллер для обработки входящих данных
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        SmtpConfig SmtpConfig { get; }
        private readonly ApplicationContext db;
        private readonly ServiceSendMail serviceSendMail;

        /// <summary>
        /// Передача зависимостей в контроллер
        /// </summary>
        /// <param name="options">Строка подключения к сервису отправки email</param>
        /// <param name="context">Контекст базы данных</param>
        /// <param name="serviceSend">Сервис отправки email</param>
        public MailsController(IOptions<SmtpConfig> options, ApplicationContext context, ServiceSendMail serviceSend)
        {
            SmtpConfig = options.Value;
            db = context;
            serviceSendMail = serviceSend;
        }

        /// <summary>
        /// При запросе /api/mails/ выводится список отправленных сообщений из баззы данных
        /// </summary>
        /// <returns>Выводит список в формате json</returns>
        [HttpGet]
        public IEnumerable<Mail> Get() => db.Mails.ToList();        

        /// <summary>
        /// Метод получает данные из формы, передает их в ServiceSendMail и сохраняет полученные данные в базе
        /// </summary>
        /// <param name="mail">Объект класса Mail</param>
        /// <returns>Ок</returns>
        [HttpPost]
        public async Task<ActionResult<Mail>> Post(Mail mail)
        {
            if (mail.Recipients == null)
                return BadRequest();

            try
            {
                await serviceSendMail.SendEmailAsync(mail, SmtpConfig);
            }
            catch(Exception ex)
            {
                mail.DateAndTime = DateTime.Now.ToString();
                mail.Result = "Failed";
                mail.FailedMessage = ex.Message;

                db.Mails.Add(mail);
                await db.SaveChangesAsync();

                return Ok(mail);                
            }
            
            
            mail.DateAndTime = DateTime.Now.ToString();
            mail.Result = "Ok";

            db.Mails.Add(mail);
            await db.SaveChangesAsync();

            return Ok(mail);            
        }
    }
}
