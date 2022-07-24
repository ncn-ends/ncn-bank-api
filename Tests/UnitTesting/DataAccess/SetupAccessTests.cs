using DataAccess;
using Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Tests.UnitTesting.DataAccess;


public class SetupAccessTests
{
    private readonly ITestOutputHelper _output;
    private readonly ISqlDataAccess _sqlDataAccess;

    public SetupAccessTests(ITestOutputHelper output)
    {
        _sqlDataAccess = new SqlDataAccess(TestingConfig.GetConfig());
        _output = output;
    }
    
    [Fact]
    public static void EnsureInitialDataWasCreated()
    {
        
    }
}