using System.Net;
using System.Net.Mail;
using System.Text;
using System.IO;
using MailKit.Net.Smtp;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

public class EmailSender
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailWithAttachmentAsync(byte[] fileBytes, string fileName)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("PDF Göndəriş Sistemi", "pmoaz123@gmail.com"));
        message.To.Add(MailboxAddress.Parse("pmoaz123@gmail.com"));
        message.Subject = "Yeni PDF faylı göndərildi";

        var builder = new BodyBuilder
        {
            TextBody = "Zəhmət olmasa əlavə edilmiş faylı yoxlayın."
        };

        builder.Attachments.Add(fileName, fileBytes);
        message.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync("pmoaz123@gmail.com", "mqocluwzxvyshrya");
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
