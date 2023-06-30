using System.Net;
using System.Net.Mail;
using dotnet_3D_store_backend.DTOs;
using dotnet_3D_store_backend.Models;

namespace dotnet_3D_store_backend.Services;
public class EmailService
{
    private readonly SmtpClient _smtpClient;

    public EmailService(EmailConfig emailConfig)
    {
        _smtpClient = new SmtpClient(emailConfig.Host, emailConfig.Port)
        {
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(emailConfig.UserName, emailConfig.Password),
            EnableSsl = true
        };
    }

    public async Task SendEmailAsync(SendEmailRequest request)
    {
        MailMessage mailMessage = new MailMessage(request.From, request.To, request.Subject, request.Body);
        await _smtpClient.SendMailAsync(mailMessage);
    }
}