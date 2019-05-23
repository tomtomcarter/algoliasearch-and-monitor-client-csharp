using System;
using Xunit;
using FluentAssertions;
using Algolia.Monitoring.Client;
using System.Threading.Tasks;

namespace Algolia.Monitoring.Test.EndToEnd
{
    public class MonitoringClientTest
    {
        [Fact]
        public async Task MonitoringClient_DummyMethodeName_ShouldSucceed()
        {
            // arrange
            var client = new MonitoringClient(Environment.GetEnvironmentVariable("ALGOLIA_APPLICATION_ID_TEST"), "ALGOLIA_API_KEY_TEST");

            // act
            var result = await client.GetCurrentStatusServerAsync();

            // assert
            result.Should().NotBeNull();
        }
    }
}
