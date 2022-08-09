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
public class AccountHolderDataAccessTests
{
    private readonly IAccountHolderAccess _accountHolderAccess;
    private readonly ISetupAccess _setupAccess;
    private readonly IAccountAccess _accountAccess;
    private readonly IAddressAccess _addressAccess;

    public AccountHolderDataAccessTests()
    {
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        _setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        _accountAccess = scope.ServiceProvider.GetRequiredService<IAccountAccess>();
        _accountHolderAccess = scope.ServiceProvider.GetRequiredService<IAccountHolderAccess>();
        _addressAccess = scope.ServiceProvider.GetRequiredService<IAddressAccess>();
    }

    [Fact]
    public async Task AccountHolderCRUDTests()
    {
        await _setupAccess.EnsureDatabaseSetup();
        
        var sampleAccountHolderData = FakeInitialData.SampleAccountHolder1;
        var holderInsertionGuid = await _accountHolderAccess.CreateOne(sampleAccountHolderData);
        holderInsertionGuid.Should().NotBeNull();
        holderInsertionGuid.Should().NotBeEmpty();

        var holder = await _accountHolderAccess.GetOne(holderInsertionGuid.Value);
        holder.Should().NotBeNull();
        holder.birthdate.Should().Be(sampleAccountHolderData.birthdate);
        holder.firstname.Should().Be(sampleAccountHolderData.firstname);
        holder.middlename.Should().Be(sampleAccountHolderData.middlename);
        holder.lastname.Should().Be(sampleAccountHolderData.lastname);
        holder.phone_number.Should().Be(sampleAccountHolderData.phone_number);
        holder.job_title.Should().Be(sampleAccountHolderData.job_title);
        holder.expected_salary.Should().Be(sampleAccountHolderData.expected_salary);
        
        await AccountHolderDeactivationAndAssertion(holder);
    }

    [Fact]
    public async Task AccountHolderGetRandomOneTests()
    {
        await _setupAccess.EnsureDatabaseSetup();
        
        var holder = await _accountHolderAccess.GetRandomOne();
        holder.Should().NotBeNull();
        holder.birthdate.Should();

        holder.firstname.Should().BeOneOf(
            FakeInitialData.SampleAccountHolder1.firstname,
            FakeInitialData.SampleAccountHolder2.firstname
            );
        
        holder.middlename.Should().BeOneOf(
            FakeInitialData.SampleAccountHolder1.middlename,
            FakeInitialData.SampleAccountHolder2.middlename);
        
        holder.lastname.Should().BeOneOf(
            FakeInitialData.SampleAccountHolder1.lastname,
            FakeInitialData.SampleAccountHolder2.lastname);
        
        holder.phone_number.Should().BeOneOf(
            FakeInitialData.SampleAccountHolder1.phone_number,
            FakeInitialData.SampleAccountHolder2.phone_number);
        
        holder.job_title.Should().BeOneOf(
            FakeInitialData.SampleAccountHolder1.job_title,
            FakeInitialData.SampleAccountHolder2.job_title);
        
        holder.expected_salary.Should().BeOneOf(
            FakeInitialData.SampleAccountHolder1.expected_salary.Value,
            FakeInitialData.SampleAccountHolder2.expected_salary.Value);
    }
    
    
    private async Task AccountHolderDeactivationAndAssertion(AccountHolderBO holder)
    {
        // assert that the addresses for the holder was deactivated

        var createdAccount = await _accountAccess.CreateOne(new AccountFormDTO
        {
            account_holder_id = holder.account_holder_id,
            account_type_key = "sta_ca",
            initial_deposit_amount = 10000
        });

        /* ensures account and addresses were created */
        var foundAccount = await _accountAccess.GetOneById(createdAccount.account_id);
        var addresses = await _addressAccess.GetAllByAccountHolder(holder.account_holder_id);

        foundAccount.account_id.Should().Be(createdAccount.account_id);
        addresses.Length().Should().Be(1);

        /* ensures account holder was deactivated */
        var deactivatedHolder = await _accountHolderAccess.DeactivateOneById(holder.account_holder_id);

        deactivatedHolder.deactivated.Should().Be(holder.account_holder_id);

        /*
         * ensures account and addresses were
         * deactivated after account holder was deactivated
         */
        var createdAccountAfterDeactivation = await _accountAccess.GetOneById(createdAccount.account_id);
        var addressesAfterDeactivation = await _addressAccess.GetAllByAccountHolder(deactivatedHolder.deactivated.Value);
        
        createdAccountAfterDeactivation.Should().BeNull();
        addressesAfterDeactivation.Length().Should().Be(0);
    }
}