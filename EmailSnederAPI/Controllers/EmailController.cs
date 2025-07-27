using Microsoft.AspNetCore.Mvc;

[Route("api/email")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly EmailSender _emailSender;

    public EmailController(EmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpPost("sendpdf")]
    public async Task<IActionResult> SendPdf([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("PDF faylı göndərilməyib.");

        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var fileBytes = ms.ToArray();

        await _emailSender.SendEmailWithAttachmentAsync(fileBytes, file.FileName);

        return Ok("PDF emailə göndərildi.");
    }
}
