using FluentAssertions;

using Microsoft.Extensions.Time.Testing;

using UrlShortener.Api.Core.Tests.TestDoubles;
using UrlShortener.Core;
using UrlShortener.Core.Urls.Add;

namespace UrlShortener.Api.Core.Tests.Urls
{
    public class AddUrlScenarios
    {
        private readonly InMemoryDataStore _urlDataStore = new InMemoryDataStore();
        private readonly AddUrlHandler _handler;
        private readonly FakeTimeProvider _timeProvider;

        public   AddUrlScenarios()
        {
            var tokenProvider = new TokenProvider();
            tokenProvider.AssignRange(1, 50);
            var shortUrlGenerator = new ShortUrlGenerator(tokenProvider);
            _timeProvider = new FakeTimeProvider();
            _handler = new AddUrlHandler(_urlDataStore, shortUrlGenerator, _timeProvider);
        }
        
        [Fact]
        public async Task Should_Return_Shortened_Url()
        {
            var request = CreateAddUrlRequest();
            var response = await _handler.HandleAsync(request, CancellationToken.None);
            
            response.Value.ShortUrl.Should().NotBeNull();
            response.Value.ShortUrl.Should().Be("1");
        }
        
        [Fact]
        public async Task Should_Save_Short_Url()
        {
            var request = CreateAddUrlRequest();
            
            var response = await _handler.HandleAsync(request, CancellationToken.None);
            
            _urlDataStore.Should().ContainKey(response.Value.ShortUrl);
        }

        [Fact]
        public async Task? Should_Save_Short_Url_With_CreatedBy_And_CreatedOn()
        {
            var request = CreateAddUrlRequest();
            
            var response = await _handler.HandleAsync(request, CancellationToken.None);

            response.Succeeded.Should().BeTrue();
            _urlDataStore.Should().ContainKey(response.Value!.ShortUrl);
            _urlDataStore[response.Value.ShortUrl].CreatedBy.Should().NotBeNullOrEmpty();
            _urlDataStore[response.Value.ShortUrl].CreatedOn.Should().Be(_timeProvider.GetUtcNow());
        }
        
        [Fact]
        public async Task? Should_Return_Error_If_CreatedBy_Is_Empty()
        {
            var request = CreateAddUrlRequest(createby: string.Empty);
            
            var response = await _handler.HandleAsync(request, CancellationToken.None);

            response.Succeeded.Should().BeFalse();
            response.Error.Code.Should().Be("MISSING_VALUE");
        }
        
        private AddUrlRequest CreateAddUrlRequest(string createby = "Caio")
        {
            return new AddUrlRequest(new Uri("https://github.com"), createby);
        }
    }
}