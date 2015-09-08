using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace ExperimentLibrary
{
    public interface IMailMessageContract
    {
        string[] Emails { get; }

        string Content { get; }
    }

    public class MailMessageContract : IMailMessageContract
    {
        public string[] Emails { get; }

        public string Content { get; }

        public MailMessageContract() { }

        public MailMessageContract(string[] emails, string content)
        {
            Emails = emails;
            Content = content;
        }
    }

    public class MailMessageConsumer : Consumes<IMailMessageContract>.Context
    {
        public void Consume(IConsumeContext<IMailMessageContract> message)
        {
            var mailMessage = message.Message;
            SendMessage(CreateMailMessage(mailMessage));
        }

        private void SendMessage(MailMessage mail)
        {
            var smtp = new SmtpClient();
            smtp.Send(mail);
        }

        private MailMessage CreateMailMessage(IMailMessageContract mailMessage)
        {
            var mail = new MailMessage
            {
                Subject = "Test",
                Body = mailMessage.Content,
                IsBodyHtml = true
            };

            foreach (var email in mailMessage.Emails)
                mail.To.Add(email);

            return mail;
        }
    }
}