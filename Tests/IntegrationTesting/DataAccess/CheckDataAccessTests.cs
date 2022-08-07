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
public class CheckDataAccessTests
{
    private ISetupAccess _setupAccess;
    private IAccountAccess _accountAccess;
    private ICheckAccess _checkAccess;

    public CheckDataAccessTests()
    {
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        _setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        _accountAccess = scope.ServiceProvider.GetRequiredService<IAccountAccess>();
        _checkAccess = scope.ServiceProvider.GetRequiredService<ICheckAccess>();
    }
    
    [Fact]
    public async Task CheckCRUDTests()
    {
        
        await _setupAccess.EnsureDatabaseSetup();
        
        var randomAccount = await _accountAccess.GetRandomOne();
        if (randomAccount is null) throw new Exception("While testing card creation, random account returned null");

        var createdCheck = await _checkAccess.CreateOne(new CheckCreationForm
        {
            account_id = randomAccount.account_id
        });
        
        await AssertCheckIsValid(createdCheck);
        
        var targetExpiration = DateTime.Now.AddMonths(6);
        var daysBetweenExpectedExpiration = (targetExpiration - createdCheck.expiration.Value).TotalDays;
        daysBetweenExpectedExpiration.Should().BeInRange(-5, 5);

        var allChecks = await _checkAccess.GetAllByAccount(randomAccount.account_id);
        
        allChecks.Should().NotBeNull();
        allChecks.Length().Should().BeOneOf(1, 2);
        
        var lastCheckInAllChecks = allChecks.LastOrDefault();
        lastCheckInAllChecks.Should().NotBeNull();
        lastCheckInAllChecks.Should().BeEquivalentTo(createdCheck);

        var deactivatedCheck = await _checkAccess.DeactivateOneById(createdCheck.check_id);

        deactivatedCheck.Should().NotBeNull();
        deactivatedCheck.deactivated.Should().NotBeEmpty();
        deactivatedCheck.deactivated.Should().Be(createdCheck.check_id);
        deactivatedCheck.account_number.Should().Be(createdCheck.account_number);
        deactivatedCheck.check_id.Should().NotBe(createdCheck.check_id);
        deactivatedCheck.routing_number.Should().NotBe(createdCheck.routing_number);
    }

    [Fact]
    public async Task ChecksByAccountHolderTests()
    {
        await _setupAccess.EnsureDatabaseSetup();

        var account = await _accountAccess.SearchByHolderName(FakeInitialData.SampleAccountHolder1.firstname);
        var checks = await _checkAccess.GetAllByAccountHolder(account.account_holder.account_holder_id);
        
        checks.Length().Should().Be(1);
        
        var firstCheck = checks.FirstOrDefault();
        
        await AssertCheckIsValid(firstCheck);

    }

    private async Task AssertCheckIsValid(CheckBO check)
    {
        check.Should().NotBeNull();
        check.check_id.Should().NotBeEmpty();
        check.account_number.Should().NotBeEmpty();
        check.account_number.Should().NotBeNull();
        check.routing_number.Should().NotBeNull();
        check.routing_number.Length.Should().Be(9);
    }
}