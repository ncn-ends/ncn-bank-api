using System.Linq;
using System.Reflection;
using DataAccess;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Tests.UnitTesting;

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
        IConfiguration config = new ConfigurationBuilder()
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .AddEnvironmentVariables()
            .Build();
        _output = output;
        _sqlDataAccess = new SqlDataAccess(config);
    }
    
    [Fact]
    public async void ExecRawSqlTests_NoParameters()
    {
        var res = await _sqlDataAccess.ExecRawSql<ExampleMappedObject>("SELECT 'Peter' AS Name, 24 AS Age;");
        res.FirstOrDefault().Should().NotBeNull();
        res.FirstOrDefault().Should().BeOfType<ExampleMappedObject>();
        res.FirstOrDefault().Age.Should().Be(24);
        res.FirstOrDefault().Name.Should().Be("Peter");
    }
    
    
    [Fact]
    public async void ExecRawSqlTests_WithParameters()
    {
        var age = 94;
        var name = "Euclid";
        var query = "SELECT @Name AS Name, @Age AS Age;";
        var queryParams = new {
            Age = age,
            Name = name
        };
        var res = await _sqlDataAccess.ExecRawSql<ExampleMappedObject, dynamic>(query, queryParams);
        res.FirstOrDefault().Should().NotBeNull();
        res.FirstOrDefault().Should().BeOfType<ExampleMappedObject>();
        res.FirstOrDefault().Age.Should().Be(age);
        res.FirstOrDefault().Name.Should().Be(name);
    }
}