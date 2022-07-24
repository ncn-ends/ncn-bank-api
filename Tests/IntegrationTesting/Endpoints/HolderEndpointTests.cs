using System;
using System.Threading.Tasks;
using DataAccess.Models;
using FluentAssertions;
using Tests.Helpers;
using Xunit;

namespace Tests.IntegrationTesting.Endpoints;

public class HolderEndpointTests
{
    [Fact]
    public async Task TestGetWithoutQueryParameter()
    {
        var client = new HttpClientBroker("/api/holder/");
        var response = await client.SendGet<AccountHolderBO>();
        response.IsSuccessStatusCode.Should().BeFalse();
    }
    
    [Fact]
    public async Task TestGetWithQueryParameter()
    {
        var client = new HttpClientBroker("/api/holder/123");
        var response = await client.SendGet<AccountHolderBO>();
        response.IsSuccessStatusCode.Should().BeFalse();
    }

    [Fact]
    public async Task TestPostWithWorkingBody()
    {
        var client = new HttpClientBroker("/api/holder");
        var sampleAccountHolderData = new AccountHolderDTO
        {
            birthdate = new DateTime(),
            firstname = "Bobby",
            middlename = "James",
            lastname = "Christopher",
            phone_number = "111-111-1111",
            job_title = "Software Dev",
            expected_salary = 1000000
        };
        var response = await client.SendPost(sampleAccountHolderData);
        response.EnsureSuccessStatusCode();
    }
}