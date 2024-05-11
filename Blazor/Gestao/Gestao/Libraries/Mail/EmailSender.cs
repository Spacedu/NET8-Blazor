using Gestao.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace Gestao.Libraries.Mail
{
    public class EmailSender(ILogger<EmailSender> logger, IConfiguration configuration, SmtpClient smtp) : IEmailSender<ApplicationUser>
    {
        private readonly ILogger logger = logger;
        private readonly SmtpClient smtp = smtp;
        private readonly IConfiguration configuration = configuration;

        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) => SendEmailAsync(email, "Confirme seu e-mail",
            $"Por favor confirme seu cadastro clicando no link: " +
            $"<a href='{confirmationLink}'>Confirme</a>.");

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) => SendEmailAsync(email, "Resetar senha",
            $"Clique no link e resete sua senha: <a href='{resetLink}'>Resetar senha</a>.");

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) => SendEmailAsync(email, "Resetar senha",
            $"Use esse código para resetar sua senha: {resetCode}");

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            await Execute(subject, message, toEmail);
        }

        public async Task Execute(string subject, string message, string toEmail)
        {
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress( configuration.GetValue<string>("EmailSender:User")! );
            mailMessage.To.Add(new MailAddress(toEmail));
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;

            await smtp.SendMailAsync(mailMessage);
            
            logger.LogInformation("Email to {EmailAddress} sent!", toEmail);
        }
    }
}
