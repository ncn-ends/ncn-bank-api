using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DataAccess.Access;
using DataAccess.Models;
using DataAccess.Setup;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Tests.Helpers;
using Xunit;

namespace Tests.UnitTesting.DataAccess;

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

        var createdCheck = await checkAccess.CreateOne(new CheckCreationForm()
        {
            account_id = randomAccount.account_id
        });

        createdCheck.Should().NotBeNull();
        createdCheck.check_id.Should().NotBeEmpty();
        createdCheck.account_number.Should().NotBeEmpty();
        createdCheck.account_number.Should().NotBeNull();
        createdCheck.expiration.Should().BeAfter(1.March(2023));
        createdCheck.routing_number.Should().NotBeNull();
        createdCheck.routing_number.Length.Should().Be(9);
    }
}