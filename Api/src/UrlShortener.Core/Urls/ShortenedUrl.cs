namespace UrlShortener.Core.Urls
{
    public class ShortenedUrl(Uri longUrl, string shortUrl, string createdByBy, DateTimeOffset createdOn)
    {
        public Uri LongUrl { get; } = longUrl;
        public string ShortUrl { get; } = shortUrl;
        public string CreatedBy { get; } = createdByBy;
        public DateTimeOffset CreatedOn { get; } = createdOn;
    }
}