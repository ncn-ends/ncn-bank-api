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
public class AccountDataAccessTests
{
    private readonly ISetupAccess _setupAccess;
    private readonly IAccountHolderAccess _accountHolderAccess;
    private readonly IAccountAccess _accountAccess;
    private readonly ICardAccess _cardAccess;
    private readonly ICheckAccess _checkAccess;

    public AccountDataAccessTests()
    {
        
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        _setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        _accountHolderAccess = scope.ServiceProvider.GetRequiredService<IAccountHolderAccess>();
        _accountAccess = scope.ServiceProvider.GetRequiredService<IAccountAccess>();
        _cardAccess = scope.ServiceProvider.GetRequiredService<ICardAccess>();
        _checkAccess = scope.ServiceProvider.GetRequiredService<ICheckAccess>();
    }
    [Fact]
    public async Task AccountCRUDTests()
    {
        
        await _setupAccess.EnsureDatabaseSetup();

        var holderInsertionGuid = await _accountHolderAccess.CreateOne(FakeInitialData.SampleAccountHolder1);
        
        if (holderInsertionGuid is null) throw new Exception("AccountHolder creation insertion failed. Re-run those tests.");

        var sampleAccountForm = new AccountFormDTO
        {
            account_holder_id = holderInsertionGuid.Value,
            account_type_key = "stu_ca",
            initial_deposit_amount = 10000
        };
        var account = await _accountAccess.CreateOne(sampleAccountForm);
        account.Should().NotBeNull();
        account.account_id.Should().NotBeEmpty();
        account.account_number.ToString().Length.Should().Be(9);
        account.routing_number.ToString().Length.Should().Be(9);

        var holder = await _accountAccess.GetOneById(account.account_id);
        holder.Should().NotBeNull();
        holder.account_holder_id.Should().NotBeEmpty();
        holder.account_id.Should().NotBeEmpty();
        holder.account_number.Should().NotBe(-1);
        holder.routing_number.Should().NotBe(-1);

        var initialBalance = await _accountAccess.GetAccountBalance(account.account_id);
        initialBalance.balance.Should().Be(sampleAccountForm.initial_deposit_amount);

        await AccountDeactivationAndAssertion(account);
    }

    private async Task AccountDeactivationAndAssertion(AccountInsertionReturn account)
    {
        var cardsToCreate = new[]
        {
            new CardCreationForm
            {
                account_id = account.account_id,
                pin_number = "1111"
            },
            new CardCreationForm
            {
                account_id = account.account_id,
                pin_number = "2222"
            },
            new CardCreationForm
            {
                account_id = account.account_id,
                pin_number = "3333"
            },
        };

        foreach (var cardForm in cardsToCreate)
        {
            await _cardAccess.CreateOne(cardForm);
        }

        var createdCards = await _cardAccess.GetAllByAccount(account.account_id);

        createdCards.Length().Should().Be(3);
        createdCards.FirstOrDefault().pin_number.Should().Be("1111");
        createdCards.LastOrDefault().pin_number.Should().Be("3333");
        
        var checksToCreate = new[]
        {
            new CheckCreationForm
            {
                account_id = account.account_id
            },
            new CheckCreationForm
            {
                account_id = account.account_id
            },
            new CheckCreationForm
            {
                account_id = account.account_id
            }
        };
        
        foreach (var checkForm in checksToCreate)
        {
            await _checkAccess.CreateOne(checkForm);
        }
        
        var createdChecks = await _cardAccess.GetAllByAccount(account.account_id);

        createdChecks.Length().Should().Be(3);

        var deactivatedAccount = await _accountAccess.DeactivateOneById(account.account_id);
        
        deactivatedAccount.deactivated.Should().Be(account.account_id);

        var activeCardsAfterDeactivating = await _cardAccess.GetAllByAccount(deactivatedAccount.deactivated);
        activeCardsAfterDeactivating.Length().Should().Be(0);

        var activeChecksAfterDeactivating = await _checkAccess.GetAllByAccount(deactivatedAccount.deactivated);
        activeChecksAfterDeactivating.Length().Should().Be(0);
    }
}