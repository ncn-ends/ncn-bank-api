using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Tests.IntegrationTesting;

public class HealthEndpointTests
{
    [Fact]
    public async Task TestPingEndpoint()
    {
        await using var app = new WebApplicationFactory<Program>();
        using var client = app.CreateClient();

        var response = await client.GetAsync("/health/ping");
        var content = response.Content.ReadAsStringAsync();

        var status = response.StatusCode;
        
        status.Should().Be(HttpStatusCode.OK);
        content.Result.Should().Be("\"pong\"");
    }
}