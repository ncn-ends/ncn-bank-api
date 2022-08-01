using System;
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
    [Fact]
    public async Task CheckCRUDTests()
    {
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        var setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        var accountAccess = scope.ServiceProvider.GetRequiredService<IAccountAccess>();
        var checkAccess = scope.ServiceProvider.GetRequiredService<ICheckAccess>();
        await setupAccess.EnsureDatabaseSetup();
        
        var randomAccount = await accountAccess.GetRandomOne();
        if (randomAccount is null) throw new Exception("While testing card creation, random account returned null");

        var createdCheck = await checkAccess.CreateOne(new CheckCreationForm
        {
            account_id = randomAccount.account_id
        });

        createdCheck.Should().NotBeNull();
        createdCheck.check_id.Should().NotBeEmpty();
        createdCheck.account_number.Should().NotBeEmpty();
        createdCheck.account_number.Should().NotBeNull();
        createdCheck.routing_number.Should().NotBeNull();
        createdCheck.routing_number.Length.Should().Be(9);
        
        var targetExpiration = DateTime.Now.AddMonths(6);
        var daysBetweenExpectedExpiration = (targetExpiration - createdCheck.expiration.Value).TotalDays;
        daysBetweenExpectedExpiration.Should().BeInRange(-5, 5);

        var allChecks = await checkAccess.GetAllByAccount(randomAccount.account_id);
        allChecks.Should().NotBeNull();
        allChecks.Length().Should().BeOneOf(1, 2); // TODO: why does this become 2 sometimes?
        var firstCheckInAllChecks = allChecks.FirstOrDefault();
        firstCheckInAllChecks.Should().NotBeNull();
        firstCheckInAllChecks.Should().BeEquivalentTo(createdCheck);
        
        

        var deactivatedCheck = await checkAccess.DeactivateOneById(createdCheck.check_id);
        
        deactivatedCheck.Should().NotBeNull();
        deactivatedCheck.deactivated.Should().BeTrue();
        deactivatedCheck.account_number.Should().Be(createdCheck.account_number);
        
        deactivatedCheck.check_id.Should().NotBe(createdCheck.check_id);
        deactivatedCheck.routing_number.Should().NotBe(createdCheck.routing_number);
    }
}