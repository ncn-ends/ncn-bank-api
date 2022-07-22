using System.Diagnostics;
using FileReaderLib;
using System.IO;

namespace DataAccess.Access;

public interface ISetupAccess
{
    Task EnsureDatabaseSetup();
}

public class SetupAccess: ISetupAccess
{
    private readonly ISqlDataAccess _dataAccess;

    public SetupAccess(ISqlDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task EnsureDatabaseSetup()
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            await DatabaseDropAll();
        }
        await SetupCustomTypes();
        await SetupUtilities();
        await SetupTables();
        await SetupSubroutines();

    }

    private async Task DatabaseDropAll()
    {
        var dbResetScriptPath = "Database/Setup/Reset.sql";
        var resetScriptFileContents = FileReader.ReadFile(dbResetScriptPath);
        await ExecuteFile(resetScriptFileContents);
    }

    private async Task SetupCustomTypes()
    {
        var dbTypesPath = "Database/Types";
        var files = FileReader.ReadDirectory(dbTypesPath);
        await ExecuteFiles(files);
    }

    private async Task SetupUtilities()
    {
        var dbUtilitiesPath = "Database/Utilities";
        var files = FileReader.ReadDirectory(dbUtilitiesPath);
        await ExecuteFiles(files);
    }
    
    private async Task SetupSubroutines()
    {
        var dbSrPath = "Database/Subroutines";
        var files = FileReader.ReadDirectory(dbSrPath);
        await ExecuteFiles(files);
    }
    
    private async Task SetupTables()
    {
        var dbTablesPath = "Database/Tables";
        var executionOrder = new[]
        {
            "transfer_limits",
            "monthly_fees",
            "account_types",
            "account_holders",
            "accounts",
            "addresses",
            "cards",
            "checks",
            "transfers",
            "account_links",
            "branches"
        };
        
        var files = FileReader.ReadDirectory(dbTablesPath);
        
        await ExecuteFiles(files, executionOrder);
    }

    private async Task ExecuteFiles(IEnumerable<FileData> files)
    {
        files = files.ToArray();
        foreach (var file in files)
        {
            await ExecuteFileFromPath(file.FilePath);
        }
    }

    private async Task ExecuteFiles(IEnumerable<FileData> files, IEnumerable<string> executionOrder)
    {
        files = files.ToArray();
        foreach (var key in executionOrder)
        {
            var matchedFile = files.Single<FileData>(el => el.FileName.Contains(key));
            await ExecuteFileFromPath(matchedFile.FilePath);
        }
    }
    
    private async Task ExecuteFile(string fileContents) => await _dataAccess.ExecRawSql(fileContents);

    private async Task ExecuteFileFromPath(string path)
    {
        var fileContents = File.ReadAllText(path);
        await _dataAccess.ExecRawSql(fileContents);
    }
}