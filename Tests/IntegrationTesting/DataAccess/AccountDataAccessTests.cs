using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Access;
using DataAccess.Models;
using FluentAssertions;
using Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Tests.UnitTesting.DataAccess;

[Collection("SequentialTesting")]
public class AccountDataAccessTests
{
    private readonly IAccountAccess _accountAccess;
    private readonly IAccountHolderAccess _accountHolderAccess;
    private readonly CustomWAF<Program> _waf = new ();


    public AccountDataAccessTests()
    {
        var dataAccess = new SqlDataAccess(TestingConfig.GetConfig());
        _accountAccess = new AccountAccess(dataAccess);
        _accountHolderAccess = new AccountHolderAccess(dataAccess);
    }

    [Fact]
    public async Task AccountCRUD()
    {
        
        var sampleAccountHolderData = new AccountHolderDTO
        {
            birthdate = new DateTime(),
            firstname = "Bobby",
            middlename = "James",
            lastname = "Christopher",
            phone_number = "111-111-1111",
            job_title = "Software Dev",
            expected_salary = 1000000
        };
        var holderInsertionGuid = await _accountHolderAccess.CreateOne(sampleAccountHolderData);
        
        if (holderInsertionGuid is null) throw new Exception("AccountHolder creation insertion failed. Re-run those tests.");
        
        var sampleAccountForm = new AccountFormDTO
        {
            account_holder_id = holderInsertionGuid.Value,
            account_type_key = "stu_ca",
            initial_deposit = 10000
        };
        var accountInfo = await _accountAccess.CreateOne(sampleAccountForm);
        accountInfo.Should().NotBeNull();
        accountInfo.account_id.Should().NotBeEmpty();
        accountInfo.account_number.ToString().Length.Should().Be(9);
        accountInfo.routing_number.ToString().Length.Should().Be(9);
        //
        // var holder = await _accountAccess.GetOne(holderInsertionGuid.Value);
        // holder.Should().NotBeNull();
        // holder.birthdate.Should().Be(sampleAccountHolderData.birthdate);
        // holder.firstname.Should().Be(sampleAccountHolderData.firstname);
        // holder.middlename.Should().Be(sampleAccountHolderData.middlename);
        // holder.lastname.Should().Be(sampleAccountHolderData.lastname);
        // holder.phone_number.Should().Be(sampleAccountHolderData.phone_number);
        // holder.job_title.Should().Be(sampleAccountHolderData.job_title);
        // holder.expected_salary.Should().Be(sampleAccountHolderData.expected_salary);
    }
}