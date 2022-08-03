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
public class TransferDataAccessTests
{
    private readonly ISetupAccess _setupAccess;
    private readonly IAccountAccess _accountAccess;
    private readonly ITransferAccess _transferAccess;
    private readonly ICheckAccess _checkAccess;
    private readonly ICardAccess _cardAccess;
    private readonly IAccountHolderAccess _holderAccess;

    public TransferDataAccessTests()
    {
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        _setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        _accountAccess = scope.ServiceProvider.GetRequiredService<IAccountAccess>();
        _transferAccess = scope.ServiceProvider.GetRequiredService<ITransferAccess>();
        _checkAccess = scope.ServiceProvider.GetRequiredService<ICheckAccess>();
        _cardAccess = scope.ServiceProvider.GetRequiredService<ICardAccess>();
        _holderAccess = scope.ServiceProvider.GetRequiredService<IAccountHolderAccess>();
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

    [Fact]
    public async Task TransferAggregationTests()
    {
        await _setupAccess.EnsureDatabaseSetup();
        
        var sourceAccount = await _accountAccess.SearchByHolderName(FakeInitialData.SampleAccountHolder1.lastname);
        var targetAccount = await _accountAccess.SearchByHolderName(FakeInitialData.SampleAccountHolder2.lastname);
        var randomCheck = await _checkAccess.GetRandomOne();
        var randomCard = await _cardAccess.GetRandomOne();
        
        var initialTransfers = await _transferAccess.GetAllByTargetAccount(targetAccount.account_id);
        var initialTransfer = initialTransfers.FirstOrDefault();

        var checkTransferAmounts = new[]
        {
            3147.85m,
            9438.12m,
            1239.55m,
            5985.58m,
            8574.21m,
            23.57m,
            485.59m
        };
        var cardTransferAmounts = new[]
        {
            5250.77m,
            535.11m,
            896.49m,
            4145.99m,
            6859.75m,
            7746.85m
        };
        var cashTransferAmounts = new[]
        {
            942.71m,
            5122.43m,
            1883.76m,
            3877.72m,
            3251.46m,
            4573.95m,
            7107.09m,
            5914.02m,
            6028.79m
        };
        var totalTransferCount =        checkTransferAmounts.Length
                                        + cardTransferAmounts.Length
                                        + cashTransferAmounts.Length;
        
        var totalTransferAmountNoCash = checkTransferAmounts.Sum()
                                        + cardTransferAmounts.Sum();
        
        var totalTransferAmount =       totalTransferAmountNoCash
                                        + cashTransferAmounts.Sum();

        for (int i = 0; i < checkTransferAmounts.Length; i++)
        {
            var transferForm = new CheckTransferForm
            {
                amount = checkTransferAmounts[i],
                routing_number = randomCheck.routing_number,
                transfer_target = targetAccount.account_id
            };
            await _transferAccess.MakeTransfer(transferForm);
        }
        
        for (int i = 0; i < cardTransferAmounts.Length; i++)
        {
            var transferForm = new CardTransferForm
            {
                amount = cardTransferAmounts[i],
                card_number = randomCard.card_number,
                transfer_target = targetAccount.account_id
            };
            await _transferAccess.MakeTransfer(transferForm);
        }
        
        for (int i = 0; i < cashTransferAmounts.Length; i++)
        {
            var transferForm = new CashTransferForm
            {
                amount = cashTransferAmounts[i],
                transfer_target = targetAccount.account_id
            };
            await _transferAccess.MakeTransfer(transferForm);
        }

        var allTransfersByTarget = await _transferAccess.GetAllByTargetAccount(targetAccount.account_id);
        var allTransfersBySource = await _transferAccess.GetAllBySourceAccount(sourceAccount.account_id);

        allTransfersByTarget.Should().NotBeNull();
        var initialTransferCount = 1;
        allTransfersByTarget.Length().Should().Be(totalTransferCount + initialTransferCount);

        allTransfersBySource.Should().NotBeNull();
        var allTransfersBySourceExpectedLength = totalTransferCount - cashTransferAmounts.Length;
        allTransfersBySource.Length().Should().Be(allTransfersBySourceExpectedLength);

        var targetAccountBalance = await _accountAccess.GetAccountBalance(targetAccount.account_id);
        var sourceAccountBalance = await _accountAccess.GetAccountBalance(sourceAccount.account_id);

        var initialSourceAccountBalance = 300;
        
        targetAccountBalance.balance.Should().Be(totalTransferAmount + initialTransfer.amount);
        sourceAccountBalance.balance.Should().Be(initialSourceAccountBalance - totalTransferAmountNoCash);
        
        var targetHolderBalance = await _holderAccess.GetBalance(targetAccount.account_holder.account_holder_id);
        var sourceHolderBalance = await _holderAccess.GetBalance(sourceAccount.account_holder.account_holder_id);
        
        targetHolderBalance.balance.Should().Be(targetAccountBalance.balance);
        sourceHolderBalance.balance.Should().Be(sourceAccountBalance.balance);

        var targetAllTransfers = await _transferAccess.GetAllByAccountHolder(targetAccount.account_holder.account_holder_id);
        var sourceAllTransfers = await _transferAccess.GetAllByAccountHolder(sourceAccount.account_holder.account_holder_id);
        targetAllTransfers.Length().Should().Be(allTransfersByTarget.Length());
        sourceAllTransfers.Length().Should().Be(allTransfersBySourceExpectedLength + 1);
    }
}