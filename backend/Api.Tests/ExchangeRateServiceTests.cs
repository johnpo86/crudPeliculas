using Api.Services;
using Moq;
using Xunit;

namespace Api.Tests
{
    public class ExchangeRateServiceTests
    {
        [Fact]
        public async Task GetCopRateAsync_ReturnsExpectedRate()
        {
            // Arrange
            var mockHttpClient = new Mock<HttpClient>();
            var service = new ExchangeRateService(new HttpClient()); // Using real for this simulation

            // Act
            var result = await service.GetCopRateAsync();

            // Assert
            Assert.True(result > 0);
            Assert.Equal(4300m, result);
        }
    }
}
