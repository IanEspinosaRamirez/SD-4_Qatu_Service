using System.Net;
using System.Net.Mail;
using Domain.Email;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class EmailService : IEmailService {
  private readonly EmailSettings _emailSettings;

  public EmailService(IConfiguration configuration) {
    _emailSettings =
        configuration.GetSection("EmailSettings").Get<EmailSettings>();
  }

  public async Task SendEmail(string toEmail, string subject, string body) {
    using (var smtpClient = new SmtpClient(_emailSettings.SmtpHost,
                                           _emailSettings.SmtpPort)) {
      smtpClient.EnableSsl = _emailSettings.EnableSsl;
      smtpClient.Credentials = new NetworkCredential(_emailSettings.UserName,
                                                     _emailSettings.Password);

      var mailMessage =
          new MailMessage { From = new MailAddress(_emailSettings.FromEmail),
                            Subject = subject, Body = body, IsBodyHtml = true };
      mailMessage.To.Add(toEmail);

      await smtpClient.SendMailAsync(mailMessage);
    }
  }

  public async Task SendEmailArchive(string toEmail, string subject,
                                     string body,
                                     List<string> attachmentPaths) {
    using (var smtpClient = new SmtpClient(_emailSettings.SmtpHost,
                                           _emailSettings.SmtpPort)) {
      smtpClient.EnableSsl = _emailSettings.EnableSsl;
      smtpClient.Credentials = new NetworkCredential(_emailSettings.UserName,
                                                     _emailSettings.Password);

      var mailMessage =
          new MailMessage { From = new MailAddress(_emailSettings.FromEmail),
                            Subject = subject, Body = body, IsBodyHtml = true };
      mailMessage.To.Add(toEmail);

      // Lógica de adjuntos para SendEmailArchive
      if (attachmentPaths != null && attachmentPaths.Count > 0) {
        foreach (var attachmentPath in attachmentPaths) {
          if (File.Exists(attachmentPath)) {
            var attachment = new Attachment(attachmentPath);
            mailMessage.Attachments.Add(attachment);
          }
        }
      }

      await smtpClient.SendMailAsync(mailMessage);
    }
  }
}
