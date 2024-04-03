using EmailsSender.Abstractions;
using System.Net.Mail;
using System.Net;

namespace EmailsSender.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _host;
        private readonly string _username;
        private readonly string _password;

        public EmailSender(string host, string username, string password)
        {
            _host = host;
            _username = username;
            _password = password;
        }

        public Task SendAsync(string email, string subject, string message, CancellationToken cancellationToken)
        {
            MailAddress to = new MailAddress(email);
            MailAddress from = new MailAddress(_username);

            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = subject;
            mailMessage.Body = message;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = _host;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(_username, _password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            return smtp.SendMailAsync(mailMessage, cancellationToken);
        }
    }
}
