using System.Net;
using System.Net.Mail;
using System.Text;
using System.IO;

public class EmailSender
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailWithAttachmentAsync(byte[] fileBytes, string fileName)
    {
        var fromEmail = "pmoaz123@gmail.com";
        var fromPassword = "umoxojtjturwmdkv";
        var toEmail = "pmoaz123@gmail.com";

        var fromAddress = new MailAddress(fromEmail, "PDF Göndəriş Sistemi");
        var toAddress = new MailAddress(toEmail);

        try
        {
            using var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Yeni PDF faylı göndərildi",
                Body = "Zəhmət olmasa əlavə edilmiş faylı yoxlayın.",
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = false
            };

            message.Attachments.Add(new Attachment(new MemoryStream(fileBytes), fileName));
            await smtp.SendMailAsync(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Mail xətası: {ex.Message}");
            throw;
        }
    }
}
