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

public class CardEndpointTests
{
    [Fact]
    public async Task CardCRUDEndpointTests()
    {
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        var setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        var accountAccess = scope.ServiceProvider.GetRequiredService<IAccountAccess>();
        
        await setupAccess.EnsureDatabaseSetup();
        
        var randomAccount = await accountAccess.GetRandomOne();
        if (randomAccount is null) throw new Exception("Sample Account Holder wasn't created properly");

        var client = new HttpClientBroker("/api/card", injectedClient: waf.CreateClient());

        var sampleCard = FakeInitialData.SampleCard1(randomAccount.account_id);
        var postRes = await client.SendPost(sampleCard);
        postRes.EnsureSuccessStatusCode();

        var postContent = await JsonMapper.MapHttpContentAs<CardInsertionReturn>(postRes);

        postContent.Should().NotBeNull();
        postContent.card_id.Should().NotBeEmpty();
        postContent.card_number.Length.Should().Be(16);
        postContent.csv.Length.Should().Be(3);
        postContent.expiration.Should().BeAfter(1.January(2027));
        postContent.pin_number.Should().Be(sampleCard.pin_number);
    }
}