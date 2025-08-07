using FluentAssertions;

using UrlShortener.Core;

namespace UrlShortener.Api.Core.Tests
{
    public class TokenRangeScenarios
    {
        [Fact]
        public void When_Start_Tokan_Is_Greater_Then_End_Token_Then_Throws_Execption()
        {
            // Arrange
            var start = 10;
            var end = 5;

            // Act
            Action act = () => new TokenRange(start, end);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("End token must be greater than or equal to start token.");
        }
    }
}