using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Tests.IntegrationTesting;

public class PersonEndpointTests
{
    [Fact]
    public async Task TestPersonEndpoint()
    {
        await using var app = new WebApplicationFactory<Program>();
        using var client = app.CreateClient();

        var response = await client.GetAsync("/api/persons");
        response.EnsureSuccessStatusCode();
        var contentString = await response.Content.ReadAsStringAsync();

        var persons = JsonSerializer.Deserialize<List<PersonBO>>(contentString);
        persons.Should().HaveCount(3);
    }
}