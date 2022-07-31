using System;
using System.Threading.Tasks;
using DataAccess.Access;
using DataAccess.Setup;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Tests.Helpers;
using Xunit;

namespace Tests.IntegrationTesting.DataAccess;

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
        createdCard.pin_number.Should().Be(sampleCard.pin_number);
        
        var targetExpiration = DateTime.Now.AddMonths(54);
        var daysBetweenExpectedExpiration = (targetExpiration - createdCard.expiration).TotalDays;
        daysBetweenExpectedExpiration.Should().BeInRange(-15, 15);

        var deactivatedCard = await cardAccess.DeactivateOneById(createdCard.card_id);
        deactivatedCard.Should().NotBeNull();
        deactivatedCard.deactivated.Should().BeTrue();
        deactivatedCard.card_id.Should().NotBeEmpty();
        deactivatedCard.card_number.Length.Should().Be(16);
        deactivatedCard.csv.Length.Should().Be(3);
        deactivatedCard.pin_number.Should().Be(createdCard.pin_number);
    }
}