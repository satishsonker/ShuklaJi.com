using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace ShuklaJi.EmailManager
{
   public class EmailService 
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void Send(EmailMessage emailMessage)
        {
            var credentials = new NetworkCredential(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
            // Mail message
            var mail = new MailMessage()
            {
                From = new MailAddress(_emailConfiguration.SmtpUsername),
                Subject = emailMessage.Subject,
                Body = emailMessage.Content
            };
            mail.IsBodyHtml = true;
            foreach (var toAddresses in emailMessage.ToAddresses)
            {
                mail.To.Add(new MailAddress(toAddresses.Address));
            }
           
            // Smtp client
            var client = new SmtpClient()
            {
                Port = _emailConfiguration.SmtpPort,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = _emailConfiguration.SmtpServer,
                EnableSsl = true,
                Credentials = credentials
            };
            client.Send(mail);
            //var message = new MimeMessage();
            //message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            //message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

            //message.Subject = emailMessage.Subject;
            ////We will say we are sending HTML. But there are options for plaintext etc. 
            //message.Body = new TextPart(TextFormat.Html)
            //{
            //    Text = emailMessage.Content
            //};

            ////Be careful that the SmtpClient class is the one from Mailkit not the framework!
            //using (var emailClient = new SmtpClient())
            //{
            //    //The last parameter here is to use SSL (Which you should!)
            //    emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);

            //    //Remove any OAuth functionality as we won't be using it. 
            //    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

            //    emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

            //    emailClient.Send(message);

            //    emailClient.Disconnect(true);
            //}

        }
    }
}
