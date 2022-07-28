using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DataAccess.Access;
using DataAccess.Models;
using DataAccess.Setup;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Tests.Helpers;
using Xunit;

namespace Tests.IntegrationTesting.Endpoints;

public class CheckEndpointTests
{
    [Fact]
    public async Task CheckCRUDEndpointTests()
    {
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        var setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        var accountAccess = scope.ServiceProvider.GetRequiredService<IAccountAccess>();
        
        await setupAccess.EnsureDatabaseSetup();
        
        var randomAccount = await accountAccess.GetRandomOne();
        if (randomAccount is null) throw new Exception("Sample Account Holder wasn't created properly");

        var client = new HttpClientBroker("/api/check", injectedClient: waf.CreateClient());

        var sampleCheck = new CheckCreationForm
        {
            account_id = randomAccount.account_id
        };
        var postRes = await client.SendPost(sampleCheck);
        postRes.EnsureSuccessStatusCode();

        var postContent = await JsonMapper.MapHttpContentAs<CheckInsertionReturn>(postRes);
        
        postContent.Should().NotBeNull();
        postContent.check_id.Should().NotBeEmpty();
        postContent.account_number.Should().NotBeEmpty();
        postContent.account_number.Should().NotBeNull();
        postContent.routing_number.Should().NotBeNull();
        postContent.routing_number.Length.Should().Be(9);
        
        var targetExpiration = DateTime.Now.AddMonths(6);
        var daysBetweenExpectedExpiration = (targetExpiration - postContent.expiration.Value).TotalDays;
        daysBetweenExpectedExpiration.Should().BeInRange(-5, 5);
    }
}