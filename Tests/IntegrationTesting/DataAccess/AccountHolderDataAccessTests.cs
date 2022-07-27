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
public class AccountHolderDataAccessTests
{
    private readonly IAccountHolderAccess _accountHolderAccess;
    
    public AccountHolderDataAccessTests()
    {
        var dataAccess = new SqlDataAccess(TestingConfig.GetConfig());
        _accountHolderAccess = new AccountHolderAccess(dataAccess);
    }

    [Fact]
    public async Task AccountHolderCRUDTests()
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
}