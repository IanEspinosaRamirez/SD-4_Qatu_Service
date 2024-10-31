namespace Domain.Email;

public interface IEmailService {
  Task SendEmail(string toEmail, string subject, string body);
  Task SendEmailArchive(string toEmail, string subject, string body,
                        List<string> attachmentPaths);
}
