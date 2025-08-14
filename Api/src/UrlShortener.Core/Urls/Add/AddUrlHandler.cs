namespace UrlShortener.Core.Urls.Add
{
    public record AddUrlRequest(Uri LongUrl, string CreatedBy);
    public record AddUrlResponse(Uri LongUrl, string ShortUrl);

    public class AddUrlHandler(IUrlDataStore urlDataStore, ShortUrlGenerator shortUrlGenerator, TimeProvider timeProvider)
    {
        public async Task<Result<AddUrlResponse>> HandleAsync(AddUrlRequest request, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(request.CreatedBy))
                return Errors.MissingValue("CreatedBy");
            
            var shortened = new ShortenedUrl(
                request.LongUrl, 
                shortUrlGenerator.GenerateUniqueUrl(), 
                request.CreatedBy,
                timeProvider.GetUtcNow());
            
            await urlDataStore.AddAsync(shortened, ct);
            
            return new AddUrlResponse(request.LongUrl, shortUrlGenerator.GenerateUniqueUrl());
        }
    }
}