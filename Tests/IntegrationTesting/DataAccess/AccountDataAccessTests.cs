using System;
using System.Diagnostics;
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
    [Fact]
    public async Task AccountCRUDTests()
    {
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        var setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        var accountHolderAccess = scope.ServiceProvider.GetRequiredService<IAccountHolderAccess>();
        var accountAccess = scope.ServiceProvider.GetRequiredService<IAccountAccess>();
        var transferAccess = scope.ServiceProvider.GetRequiredService<ITransferAccess>();
        await setupAccess.EnsureDatabaseSetup();

        var holderInsertionGuid = await accountHolderAccess.CreateOne(FakeInitialData.SampleAccountHolder1);
        
        if (holderInsertionGuid is null) throw new Exception("AccountHolder creation insertion failed. Re-run those tests.");

        var sampleAccountForm = new AccountFormDTO
        {
            account_holder_id = holderInsertionGuid.Value,
            account_type_key = "stu_ca",
            initial_deposit_amount = 10000
        };
        var accountInfo = await accountAccess.CreateOne(sampleAccountForm);
        accountInfo.Should().NotBeNull();
        accountInfo.account_id.Should().NotBeEmpty();
        accountInfo.account_number.ToString().Length.Should().Be(9);
        accountInfo.routing_number.ToString().Length.Should().Be(9);

        var holder = await accountAccess.GetOneById(accountInfo.account_id);
        holder.Should().NotBeNull();
        holder.account_holder_id.Should().NotBeEmpty();
        holder.account_id.Should().NotBeEmpty();
        holder.account_number.Should().NotBe(-1);
        holder.routing_number.Should().NotBe(-1);

        var initialBalance = await accountAccess.GetAccountBalance(accountInfo.account_id);
        initialBalance.balance.Should().Be(sampleAccountForm.initial_deposit_amount);
    }
}