using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using ReminderBot.Data;
using ReminderBot.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Services.Services.Implementations
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _configuration;

        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Send(string to, string subject, string html)
        {

            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailAccount:Email").Value));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.yandex.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailAccount:Email").Value, _configuration.GetSection("EmailAccount:Password").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
