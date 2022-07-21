using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using FluentAssertions;
using Tests.Helpers;
using Xunit;
using static FileReader.FileReader;

namespace Tests.UnitTesting;

public class FileReaderTests
{
    private readonly ISqlDataAccess _dataAccess;

    public FileReaderTests()
    {
        _dataAccess = new SqlDataAccess(TestingConfig.GetConfig());
    }
    
    [Fact]
    public async Task ReadDirectoryAndGetContent()
    {
        var files = ReadDirectory("Tests/Fixtures/ArbitrarySampleDirOfSqlFiles");

        files.Should().HaveCount(2);
        
        foreach (var file in files)
        {
            var fileContents = File.ReadAllText(file.FilePath);
            fileContents.Should().Contain("SELECT");
            var res = await _dataAccess.ExecRawSql<TestSqlDto>(fileContents);
            res.Should().HaveCount(1);
            res.FirstOrDefault().Should().NotBeNull();
            res.FirstOrDefault().msg.Should().BeOneOf(new[] {"hello", "world"});

        }
    }
}

class TestSqlDto
{
    public string msg { get; set; } = "UNSET MSG";
}
