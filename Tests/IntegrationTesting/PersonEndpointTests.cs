using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Tests.Helpers;
using Xunit;

namespace Tests.IntegrationTesting;

public class PersonEndpointTests
{
    [Fact]
    public async Task TestPersonEndpoint()
    {
        var client = new HttpClientBroker("/api/persons");
        var response = await client.SendGet<string>();
        response.EnsureSuccessStatusCode();
        
        var content = await JsonMapper.MapHttpContentAs<List<PersonBO>>(response);
        content.Should().HaveCount(3);
    }
}