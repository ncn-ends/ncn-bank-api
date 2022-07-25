using System;
using System.Diagnostics;
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
        var response = await client.SendGet();
        response.IsSuccessStatusCode.Should().BeFalse();
    }

    [Fact]
    public async Task TestGetWithBadQueryParameter()
    {
        var client = new HttpClientBroker("/api/holder/123");
        var response = await client.SendGet();
        response.IsSuccessStatusCode.Should().BeFalse();
    }

    [Fact]
    public async Task TestAccountHolderEndpoints()
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
        var postResponse = await client.SendPost(sampleAccountHolderData);
        postResponse.EnsureSuccessStatusCode();
        
        var postContent = await JsonMapper.MapHttpContentAs<AccountHolderInsertionResult>(postResponse);
        
        postContent.Should().NotBeNull();
        postContent.account_holder_id.Should().NotBeEmpty();
        postContent.account_holder_id.ToString().Should().Contain("-", Exactly.Times(4));

        var holderResponse = await client.SendGet(route: $"/{postContent.account_holder_id}");
        var getContent = await JsonMapper.MapHttpContentAs<AccountHolderBO>(holderResponse);
        getContent.Should().NotBeNull();
        getContent.account_holder_id.Should().Be(postContent.account_holder_id);
        getContent.birthdate.Should().Be(sampleAccountHolderData.birthdate);
        getContent.firstname.Should().Be(sampleAccountHolderData.firstname);
        getContent.middlename.Should().Be(sampleAccountHolderData.middlename);
        getContent.lastname.Should().Be(sampleAccountHolderData.lastname);
        getContent.phone_number.Should().Be(sampleAccountHolderData.phone_number);
        getContent.job_title.Should().Be(sampleAccountHolderData.job_title);
        getContent.expected_salary.Should().Be(sampleAccountHolderData.expected_salary);
    }
}