using System;
using System.Threading.Tasks;
using DataAccess.Access;
using DataAccess.Models;
using DataAccess.Setup;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Tests.Helpers;
using Xunit;

namespace Tests.IntegrationTesting.Endpoints;

[Collection("SequentialTesting")]
public class AccountEndpointTests
{
    [Fact]
    public async Task TestAccountHolderEndpoints()
    {
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        var setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        var holderAccess = scope.ServiceProvider.GetRequiredService<IAccountHolderAccess>();
        await setupAccess.EnsureDatabaseSetup();
        var randomHolder = await holderAccess.GetRandomOne();
        if (randomHolder is null) throw new Exception("Sample Account Holder wasn't created properly");

        var client = new HttpClientBroker("/api/account", injectedClient: waf.CreateClient());
        var sample = new AccountFormDTO
        {
            account_holder_id = randomHolder.account_holder_id,
            account_type_key = "stu_ca",
            initial_deposit_amount = 10000
        };
        var postResponse = await client.SendPost(sample);
        postResponse.EnsureSuccessStatusCode();

        var postContent = await JsonMapper.MapHttpContentAs<AccountInsertionReturn>(postResponse);

        postContent.Should().NotBeNull();
        postContent.account_id.Should().NotBeEmpty();
        postContent.routing_number.ToString().Length.Should().Be(9);
        postContent.account_number.ToString().Length.Should().Be(9);

        var getResponse = await client.SendGet(route: $"/{postContent.account_id.ToString()}");
        getResponse.EnsureSuccessStatusCode();
        var getContent = await JsonMapper.MapHttpContentAs<AccountDTO>(getResponse);

        getContent.Should().NotBeNull();
        getContent.account_holder_id.Should().NotBeEmpty();
        getContent.account_id.Should().NotBeEmpty();
        getContent.account_number.ToString().Length.Should().Be(9);
        getContent.routing_number.ToString().Length.Should().Be(9);
        getContent.account_type_id.Should().BeInRange(1, 7);
    }
}