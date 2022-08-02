using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Access;
using DataAccess.Setup;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Tests.Helpers;
using Xunit;

namespace Tests.IntegrationTesting.DataAccess;

[Collection("SequentialTesting")]
public class AddressDataAccessTests
{
    [Fact]
    public async Task AddressCRUDTests()
    {
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        var setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        var addressAccess = scope.ServiceProvider.GetRequiredService<IAddressAccess>();
        var holderAccess = scope.ServiceProvider.GetRequiredService<IAccountHolderAccess>();
        
        await setupAccess.EnsureDatabaseSetup();

        var randomHolder = await holderAccess.GetRandomOne();
        var addresses = await addressAccess.GetAllByAccountHolder(randomHolder.account_holder_id);

        addresses.Length().Should().Be(1);
    }
}