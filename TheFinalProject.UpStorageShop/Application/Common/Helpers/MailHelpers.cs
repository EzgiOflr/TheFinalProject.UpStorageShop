using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Application.Common.Helpers
{
    public class MailHelpers
    {
        public static void SendMail(string filePath, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Ezgi Oflar", "ezgioflar@gmail.com"));
            message.To.Add(new MailboxAddress("Ezgi Oflar", "ezgioflar@gmail.com"));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = body;

            builder.Attachments.Add(filePath);

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                // Connect to the Gmail SMTP server
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                // Authenticate with your Gmail account
                client.Authenticate("ezgioflar@gmail.com", "Password");

                // Send the email
                client.Send(message);

                // Disconnect from the server
                client.Disconnect(true);
            }
        }
    }
}
