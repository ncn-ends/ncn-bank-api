using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Access;
using DataAccess.Models;
using DataAccess.Setup;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Tests.Helpers;
using Xunit;

namespace Tests.IntegrationTesting.DataAccess;

[Collection("SequentialTesting")]
public class CardDataAccessTests
{
    private readonly ISetupAccess _setupAccess;
    private readonly IAccountAccess _accountAccess;
    private readonly ICardAccess _cardAccess;

    public CardDataAccessTests()
    {
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        
        _setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        _accountAccess = scope.ServiceProvider.GetRequiredService<IAccountAccess>();
        _cardAccess = scope.ServiceProvider.GetRequiredService<ICardAccess>();
    }
    [Fact]
    public async Task CardCRUDTests()
    {
        await _setupAccess.EnsureDatabaseSetup();
        
        var randomAccount = await _accountAccess.GetRandomOne();

        var sampleCard = FakeInitialData.SampleCard1(randomAccount.account_id);
        var createdCard = await _cardAccess.CreateOne(sampleCard);

        await AssertCardIsValid(createdCard);
        createdCard.pin_number.Should().Be(sampleCard.pin_number);
        
        var targetExpiration = DateTime.Now.AddMonths(54);
        var daysBetweenExpectedExpiration = (targetExpiration - createdCard.expiration).TotalDays;
        daysBetweenExpectedExpiration.Should().BeInRange(-15, 15);

        var allCards = await _cardAccess.GetAllByAccount(randomAccount.account_id);
        
        allCards.Length().Should().BeOneOf(1, 2);

        var lastCardInAllCards = allCards.LastOrDefault();
        
        lastCardInAllCards.Should().NotBeNull();
        lastCardInAllCards.Should().BeEquivalentTo(createdCard);

        var deactivatedCard = await _cardAccess.DeactivateOneById(createdCard.card_id);
        
        deactivatedCard.Should().NotBeNull();
        deactivatedCard.deactivated.Should().NotBeEmpty();
        deactivatedCard.deactivated.Should().Be(createdCard.card_id);
        deactivatedCard.card_id.Should().NotBeEmpty();
        deactivatedCard.card_number.Length.Should().Be(16);
        deactivatedCard.csv.Length.Should().Be(3);
        deactivatedCard.pin_number.Should().Be(createdCard.pin_number);
    }
    

    [Fact]
    public async Task CardsByAccountHolderTests()
    {
        await _setupAccess.EnsureDatabaseSetup();

        var account = await _accountAccess.SearchByHolderName(FakeInitialData.SampleAccountHolder1.firstname);
        var firstAccountFound = account.FirstOrDefault();
        var cards = await _cardAccess.GetAllByAccountHolder(firstAccountFound.account_holder.account_holder_id);
        
        cards.Length().Should().Be(1);
        
        var firstCheck = cards.FirstOrDefault();
        
        await AssertCardIsValid(firstCheck);

    }

    private async Task AssertCardIsValid(CardBO card)
    {
        
        card.Should().NotBeNull();
        card.card_id.Should().NotBeEmpty();
        card.card_number.Length.Should().Be(16);
        card.csv.Length.Should().Be(3);
    }
}