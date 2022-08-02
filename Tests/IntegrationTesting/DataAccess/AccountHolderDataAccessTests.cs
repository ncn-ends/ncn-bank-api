using System.Diagnostics;
using System.Threading.Tasks;
using DataAccess.Access;
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

    public AccountHolderDataAccessTests()
    {
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        _setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        _accountHolderAccess = scope.ServiceProvider.GetRequiredService<IAccountHolderAccess>();
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
}