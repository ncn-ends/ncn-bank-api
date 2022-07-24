using System.Linq;
using DataAccess;
using FluentAssertions;
using Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Tests.UnitTesting.DataAccess;

class ExampleMappedObject
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class SqlDataAccessTests
{
    private readonly ITestOutputHelper _output;
    private readonly ISqlDataAccess _sqlDataAccess;

    public SqlDataAccessTests(ITestOutputHelper output)
    {
        _sqlDataAccess = new SqlDataAccess(TestingConfig.GetConfig());
        _output = output;
    }
    
    [Fact]
    public async void ExecRawSql_NoParameters()
    {
        var res = await _sqlDataAccess.ExecRawSql<ExampleMappedObject>("SELECT 'Peter' AS Name, 24 AS Age;");
        res.FirstOrDefault().Should().NotBeNull();
        res.FirstOrDefault().Should().BeOfType<ExampleMappedObject>();
        res.FirstOrDefault().Age.Should().Be(24);
        res.FirstOrDefault().Name.Should().Be("Peter");
    }
    
    
    [Fact]
    public async void ExecRawSql_WithParameters()
    {
        var query = "SELECT @Name AS Name, @Age AS Age;";
        var queryParams = new {
            Age = 94,
            Name = "Euclid"
        };
        var res = await _sqlDataAccess.ExecRawSql<ExampleMappedObject, dynamic>(query, queryParams);
        var actual = res.FirstOrDefault();
        actual.Should().NotBeNull();
        actual.Should().BeOfType<ExampleMappedObject>();
        actual.Age.Should().Be(queryParams.Age);
        actual.Name.Should().Be(queryParams.Name);
    }
}