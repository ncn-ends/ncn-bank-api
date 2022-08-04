using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DataAccess.Access;
using DataAccess.Models;
using DataAccess.Setup;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Tests.Fixtures;
using Tests.Helpers;
using Xunit;

namespace Tests.IntegrationTesting.DataAccess;

[Collection("SequentialTesting")]
public class AddressDataAccessTests
{
    [Theory]
    [ClassData(typeof(AddressTestData))]
    public async Task AddressCRUDTests(
        string street, string zipcode, string city, string state,
        string country, int unit_number, string address_type)
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

        var entry = new AddressDTO
        {
            street = street,
            zipcode = zipcode,
            city = city,
            state = state,
            country = country,
            unit_number = unit_number,
            address_type = address_type,
            account_holder_id = randomHolder.account_holder_id
        };

        await addressAccess.AddAddress(entry);
        var addressesAfterInsertion = await addressAccess.GetAllByAccountHolder(randomHolder.account_holder_id);
        addressesAfterInsertion.Length().Should().Be(addresses.Length() + 1);
        var lastEntry = addressesAfterInsertion.LastOrDefault();
        lastEntry.Should().BeEquivalentTo(entry);
    }
}