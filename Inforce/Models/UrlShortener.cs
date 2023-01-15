namespace Inforce.Models;

public class UrlShortener
{
    public int Id { get; set; }
    public string LongUrl { get; set; }
    public string ShortUrl { get; set; }
    public string CreatedBy { get; set; }
    public string CreatorId { get; set; }
    public DateTime CreatedDate { get; set; }
}
