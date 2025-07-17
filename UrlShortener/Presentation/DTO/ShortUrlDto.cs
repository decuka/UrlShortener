namespace UrlShortener.Presentation.DTO
{
    public class ShortUrlDto
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}