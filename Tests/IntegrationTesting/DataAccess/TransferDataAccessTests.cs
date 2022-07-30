using System;
using System.Threading.Tasks;
using DataAccess.Access;
using DataAccess.Models;
using DataAccess.Setup;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Tests.Helpers;
using Xunit;

namespace Tests.UnitTesting.DataAccess;


[Collection("SequentialTesting")]
public class TransferDataAccessTests
{
    private readonly ISetupAccess _setupAccess;
    private readonly IAccountAccess _accountAccess;
    private readonly ITransferAccess _transferAccess;
    private readonly ICheckAccess _checkAccess;
    private readonly ICardAccess _cardAccess;
    
    public TransferDataAccessTests()
    {
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        _setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        _accountAccess = scope.ServiceProvider.GetRequiredService<IAccountAccess>();
        _transferAccess = scope.ServiceProvider.GetRequiredService<ITransferAccess>();
        _checkAccess = scope.ServiceProvider.GetRequiredService<ICheckAccess>();
        _cardAccess = scope.ServiceProvider.GetRequiredService<ICardAccess>();
    }
    
    [Fact]
    public async Task CheckTransferCRUDTests()
    {
        await _setupAccess.EnsureDatabaseSetup();

        var targetAccount = await _accountAccess.SearchByHolderName(FakeInitialData.SampleAccountHolder2.lastname);
        var randomCheck = await _checkAccess.GetRandomOne();

        var transferForm = new CheckTransferForm
        {
            amount = 323.23m,
            routing_number = randomCheck.routing_number,
            transfer_target = targetAccount.account_id,
        };
        var transferMade = await _transferAccess.MakeTransfer(transferForm);

        transferMade.Should().NotBeNull();
        transferMade.amount.Should().Be(transferForm.amount);
        transferMade.memo.Should().Be("");
        transferMade.transfer_id.Should().NotBeEmpty();
    }
    
    
    [Fact]
    public async Task CardTransferCRUDTests()
    {
        await _setupAccess.EnsureDatabaseSetup();

        var targetAccount = await _accountAccess.SearchByHolderName(FakeInitialData.SampleAccountHolder2.lastname);
        var randomCard = await _cardAccess.GetRandomOne();
        if (randomCard is null || targetAccount is null)
            throw new Exception("Something was null that shouldn't've been.");

        var transferForm = new CardTransferForm
        {
            amount = 167.89m,
            card_number = randomCard.card_number,
            transfer_target = targetAccount.account_id
        };
        var transferMade = await _transferAccess.MakeTransfer(transferForm);

        transferMade.Should().NotBeNull();
        transferMade.amount.Should().Be(transferForm.amount);
        transferMade.memo.Should().Be("");
        transferMade.transfer_id.Should().NotBeEmpty();
    }
    
    
    [Fact]
    public async Task CashTransferCRUDTests()
    {
        await _setupAccess.EnsureDatabaseSetup();
        
        var targetAccount = await _accountAccess.SearchByHolderName(FakeInitialData.SampleAccountHolder2.lastname);

        var transferForm = new CashTransferForm
        {
            amount = 523.85m,
            transfer_target = targetAccount.account_id
        };
        var transferMade = await _transferAccess.MakeTransfer(transferForm);

        transferMade.Should().NotBeNull();
        transferMade.amount.Should().Be(transferForm.amount);
        transferMade.memo.Should().Be("");
        transferMade.transfer_id.Should().NotBeEmpty();
    }
}