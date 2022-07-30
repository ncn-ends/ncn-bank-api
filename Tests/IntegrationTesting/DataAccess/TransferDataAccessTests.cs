using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DataAccess.Access;
using DataAccess.Models;
using DataAccess.Setup;
using FluentAssertions;
using LanguageExt;
using Microsoft.Extensions.DependencyInjection;
using Tests.Helpers;
using Xunit;

namespace Tests.UnitTesting.DataAccess;


[Collection("SequentialTesting")]
public class TransferDataAccessTests
{
    [Fact]
    public async Task CheckTransferCRUDTests()
    {
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        var setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        var accountAccess = scope.ServiceProvider.GetRequiredService<IAccountAccess>();
        var checkAccess = scope.ServiceProvider.GetRequiredService<ICheckAccess>();
        var transferAccess = scope.ServiceProvider.GetRequiredService<ITransferAccess>();
        await setupAccess.EnsureDatabaseSetup();

        var targetAccount = await accountAccess.SearchByHolderName(FakeInitialData.SampleAccountHolder2.lastname);
        var randomCheck = await checkAccess.GetRandomOne();

        var transferForm = new CheckTransferForm
        {
            amount = 323.23m,
            routing_number = randomCheck.routing_number,
            transfer_target = targetAccount.account_id,
        };
        var transferMade = await transferAccess.MakeTransfer(transferForm);

        transferMade.Should().NotBeNull();
        transferMade.amount.Should().Be(transferForm.amount);
        transferMade.memo.Should().Be("");
        transferMade.transfer_id.Should().NotBeEmpty();
    }
    
    
    [Fact]
    public async Task CardTransferCRUDTests()
    {
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        var setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        var accountAccess = scope.ServiceProvider.GetRequiredService<IAccountAccess>();
        var cardAccess = scope.ServiceProvider.GetRequiredService<ICardAccess>();
        var transferAccess = scope.ServiceProvider.GetRequiredService<ITransferAccess>();
        await setupAccess.EnsureDatabaseSetup();

        var targetAccount = await accountAccess.SearchByHolderName(FakeInitialData.SampleAccountHolder2.lastname);
        var randomCard = await cardAccess.GetRandomOne();
        if (randomCard is null || targetAccount is null)
            throw new Exception("Something was null that shouldn't've been.");

        var transferForm = new CardTransferForm
        {
            amount = 323.23m,
            card_number = randomCard.card_number,
            transfer_target = targetAccount.account_id
        };
        var transferMade = await transferAccess.MakeTransfer(transferForm);

        transferMade.Should().NotBeNull();
        transferMade.amount.Should().Be(transferForm.amount);
        transferMade.memo.Should().Be("");
        transferMade.transfer_id.Should().NotBeEmpty();
    }
}