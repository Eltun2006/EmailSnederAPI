namespace EmailSnederAPI.PdfGnerate
{
    public class PdfSendRequest
    {
        public string ToEmail { get; set; }
        public string PdfBase64 { get; set; }
    }

}
