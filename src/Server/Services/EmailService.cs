using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Blazor5Email.Server.Services
{
    public class SmtpSettings
    {
        public string FromEmail { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }


    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string html, string from = null);
        Task SendRazorAsync(string to, string subject, string view, string from = null);
    }

    public class EmailModel
    {
        public string Title { get; set; }
    }

    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly IRazorViewToStringRenderer _renderer;

        public EmailService(IOptions<SmtpSettings> settings, IRazorViewToStringRenderer renderer)
        {
            _smtpSettings = settings.Value;
            _renderer = renderer;
        }

        public async Task SendRazorAsync(string to, string subject, string view, string from = null)
        {
            string html = await _renderer.RenderViewToStringAsync($"/Emails/{view}.cshtml", new EmailModel { Title = "Test Title" });
            await SendAsync(to, subject, html, from);
        }

        public async Task SendAsync(string to, string subject, string html, string from = null)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _smtpSettings.FromEmail));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
