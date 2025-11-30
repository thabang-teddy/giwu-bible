using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // You can store email settings in appsettings.json
        var host = _config["EmailSettings:SmtpHost"];
        var port = int.Parse(_config["EmailSettings:SmtpPort"]);
        var from = _config["EmailSettings:FromEmail"];
        var password = _config["EmailSettings:Password"];

        var smtpClient = new SmtpClient(host)
        {
            Port = port,
            Credentials = new NetworkCredential(from, password),
            EnableSsl = true
        };

        var mailMessage = new MailMessage(from, email, subject, htmlMessage)
        {
            IsBodyHtml = true
        };

        await smtpClient.SendMailAsync(mailMessage);
    }
}
