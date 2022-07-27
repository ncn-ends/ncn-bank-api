using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Access;
using DataAccess.Models;
using DataAccess.Setup;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Tests.UnitTesting.DataAccess;

[Collection("SequentialTesting")]
public class CardDataAccessTests
{
    [Fact]
    public async Task CardCRUDTests()
    {
        
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        var setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        var accountAccess = scope.ServiceProvider.GetRequiredService<IAccountAccess>();
        var cardAccess = scope.ServiceProvider.GetRequiredService<ICardAccess>();
        await setupAccess.EnsureDatabaseSetup();
        
        var randomAccount = await accountAccess.GetRandomOne();
        if (randomAccount is null) throw new Exception("While testing card creation, random account returned null");

        var sampleCard = FakeInitialData.SampleCard1(randomAccount.account_id);
        var createdCard = await cardAccess.CreateOne(sampleCard);

        createdCard.Should().NotBeNull();
        createdCard.card_id.Should().NotBeEmpty();
        createdCard.card_number.Length.Should().Be(16);
        createdCard.csv.Length.Should().Be(3);
        createdCard.expiration.Should().BeAfter(1.January(2027));
        createdCard.pin_number.Should().Be(sampleCard.pin_number);
    }
}