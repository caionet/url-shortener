using FluentAssertions;

using UrlShortener.Core;
using UrlShortener.Core.Urls.Add;

namespace UrlShortener.Api.Core.Tests.Urls
{
    public class ShortUrlGeneratorScenarios
    {
        [Theory]
        [InlineData(10001, 20000, "2bJ")]
        public void Should_Return_Short_Url_For_Zero(int start, int end, string expected)
        {
            var tokenProvider = new TokenProvider();
            tokenProvider.AssignRange(start, end);
            var shortUrlGenerator = new ShortUrlGenerator(tokenProvider);
        
            var shortUrl = shortUrlGenerator.GenerateUniqueUrl();

            shortUrl.Should().Be(expected);
        }
    }
}