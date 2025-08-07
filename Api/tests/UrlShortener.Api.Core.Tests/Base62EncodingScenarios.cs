using FluentAssertions;
using UrlShortener.Core;

namespace UrlShortener.Api.Core.Tests
{
    public class Base62EncodingScenarios
    {
        [Theory]
        [InlineData(1, "1")]
        [InlineData(20, "K")]
        [InlineData(987654321, "14q60P")]
        [InlineData(0, "")]
        [InlineData(-1, "")]
        [InlineData(62, "10")]
        [InlineData(1000, "G8")]
        [InlineData(61, "z")]
        public void Should_Encode_To_Base62(long number, string expected)
        {
            // Arrange
            number.EncodeToBase62().
                Should().
                Be(expected);
        }
    }
}