using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using System.Security.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace SendEmailWebApi.Models
{
    /// <summary>
    /// Данный класс проверяет адрес электронной почты получателя на соответствие,
    /// формирует письмо и отправляет его на почту получателю
    /// </summary>
    public class ServiceSendMail
    {
        /// <summary>
        /// Метод формирует письмо и отправляет его на почту получателю
        /// </summary>
        /// <param name="mail">Принимает объект класса Mail</param>
        /// <param name="smtpConfig">Принимает объект класса SmtpConfig</param>
        /// <returns></returns>
        public async Task SendEmailAsync(Mail mail, SmtpConfig smtpConfig)
        {

            List<string> emailsList = PatternEmail(mail);

            
            if (emailsList.Count == 0)
            {
                throw new Exception("Email does not match pattern");
            }
            else
            {
                var messageList = new List<MimeMessage>();

                foreach (string email in emailsList)
                {
                    MimeMessage mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress("Admin", smtpConfig.FromAddress));
                    mimeMessage.Subject = mail.Subject;
                    mimeMessage.To.Add(new MailboxAddress("", email));
                    mimeMessage.Body = new TextPart("plain")
                    {
                        Text = mail.Body
                    };

                    messageList.Add(mimeMessage);
                }

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpConfig.Host, smtpConfig.Port, false);
                    await client.AuthenticateAsync(smtpConfig.Username, smtpConfig.Password);

                    foreach (var message in messageList)
                    {
                        await client.SendAsync(message);
                    }

                    await client.DisconnectAsync(true);
                }
            }
        }

        /// <summary>
        /// Данный метод проверяет адрес электронной почты на соответствие 
        /// </summary>
        /// <param name="mail">Принимает объект класса Mail</param>
        /// <returns>Возвращает список с адресами электронной почты </returns>
        protected List<string> PatternEmail(Mail mail)
        {
            List<string> emailsList = new List<string>();

            string patter = @"(?![_.-])((?![_.-][_.-])[a-zA-Z\d_.-]){0,63}[a-zA-Z\d]@((?!-)((?!--)[a-zA-Z\d-]){0,63}[a-zA-Z\d]\.){1,2}([a-zA-Z]{2,14}\.)?[a-zA-Z]{2,14}";

            foreach (string email in mail.RecipientsList)
            {
                if (Regex.IsMatch(email, patter))
                {
                    emailsList.Add(email);
                }
            }

            return emailsList;
        }
    }
}
