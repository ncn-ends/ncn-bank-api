using System.Threading.Tasks;
using DataAccess;
using DataAccess.Access;
using FluentAssertions;
using Tests.Helpers;
using Xunit;

namespace Tests.UnitTesting.DataAccess;

[Collection("SequentialTesting")]
public class AccountTypeDataAccessTests
{
    private readonly IAccountTypeAccess _accountTypeAccess;
    public AccountTypeDataAccessTests()
    {
        var dataAccess = new SqlDataAccess(TestingConfig.GetConfig());
        _accountTypeAccess = new AccountTypeAccess(dataAccess, TestingConfig.GetConfig());
    }

    [Fact]
    public async Task  AccountTypesGetAll()
    {
        var accountTypes = await _accountTypeAccess.GetAllAccountTypes();
        accountTypes.Should().HaveCount(7);
    }
}