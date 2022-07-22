using System.Net;
using System.Threading.Tasks;
using DataAccess.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Tests.Helpers;
using Xunit;

namespace Tests.IntegrationTesting;

public class HealthEndpointTests
{
    [Fact]
    public async Task TestPingEndpoint()
    {
        var client = new HttpClientBroker("/health/ping");
        var response = await client.SendGet<string>();
        response.EnsureSuccessStatusCode();

        var content = await JsonMapper.MapHttpContentAs<string>(response);
        content.Should().Be("pong");
    }
}