using System;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Access;
using DataAccess.Models;
using FluentAssertions;
using Tests.Helpers;
using Xunit;

namespace Tests.UnitTesting.DataAccess;

public class AccountHolderDataAccessTests
{
    private readonly IAccountHolderAccess _accountHolderAccess;
    public AccountHolderDataAccessTests()
    {
        var dataAccess = new SqlDataAccess(TestingConfig.GetConfig());
        _accountHolderAccess = new AccountHolderAccess(dataAccess);
    }

    [Fact]
    public async Task AccountHolderInsertion()
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
    } 
}