using System.Threading.Tasks;
using FluentAssertions;
using Tests.Helpers;
using Xunit;

namespace Tests.IntegrationTesting.Endpoints;

[Collection("SequentialTesting")]
public class HealthEndpointTests
{
    [Fact]
    public async Task TestPingEndpoint()
    {
        var client = new HttpClientBroker("/health/ping");
        var response = await client.SendGet();
        response.EnsureSuccessStatusCode();

        var content = await JsonMapper.MapHttpContentAs<string>(response);
        content.Should().Be("pong");
    }
}