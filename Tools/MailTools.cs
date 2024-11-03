using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;

namespace Tools
{
    public class MailTools
    {
        public static MailMessage ConstructMailMessage(string recipient, string subject, string body)
        {
            MailAddress sender = new MailAddress(getMailConfig("senderEmail"));
            MailMessage mailMessage = new MailMessage(sender, new MailAddress(recipient));
            mailMessage.From = sender;
            mailMessage.To.Add(recipient);
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            return mailMessage;
        }
        public static SmtpClient GetSmtpClient()
        {
            string smtpHost = getMailConfig("smtpHost");
            SmtpClient smtpClient = new SmtpClient(smtpHost);
            smtpClient.Host = smtpHost;
            smtpClient.Port = int.Parse(getMailConfig("smtpPort"));
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(getMailConfig("smtpUsername"), getMailConfig("smtpPassword"));
            smtpClient.EnableSsl = true;

            return smtpClient;
        }

        private static string getMailConfig(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}
