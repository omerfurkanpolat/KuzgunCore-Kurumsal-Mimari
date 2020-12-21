using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Kuzgun.Core.Utilities.EmailService.Smtp;

namespace Kuzgun.Core.Utilities.EmailService.Smtp.Google
{
    public class GoogleEmailService:IEmailService
    {
        public void SendMail(string userName, string email, string subjectText, string callBackUrl)
        {
            var body = new StringBuilder();

            body.AppendLine("Sayın " + userName + ",\n\n");
            body.AppendLine(callBackUrl);


            var fromAddress = new MailAddress("sahikainfo@gmail.com", "Kuzgun");
            var toAddress = new MailAddress(email);
            string subject = subjectText;
            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "24042020aA+")
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body.ToString() })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}
