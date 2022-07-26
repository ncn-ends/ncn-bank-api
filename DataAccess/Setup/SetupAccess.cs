using DataAccess.Access;
using FileReaderLib;

namespace DataAccess.Setup;

public interface ISetupAccess
{
    Task EnsureDatabaseSetup();
}

public class SetupAccess: ISetupAccess
{
    private readonly ISqlDataAccess _dataAccess;
    private readonly IAccountHolderAccess _accountHolderAccess;

    public SetupAccess(ISqlDataAccess dataAccess, IAccountHolderAccess accountHolderAccess)
    {
        _dataAccess = dataAccess;
        _accountHolderAccess = accountHolderAccess;
    }

    public async Task EnsureDatabaseSetup()
    {
        var isDevEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        if (isDevEnv)
        {
            await DatabaseDropAll();
        }
        await SetupCustomTypes();
        await SetupUtilities();
        await SetupTables();
        await SetupSubroutines();
        await SetupRequiredInitialData();

        if (isDevEnv)
        {
            await SetupFakeInitialData();
        }
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
    
    private async Task SetupRequiredInitialData()
    {
        var dbInitialDataPath = "Database/Setup/CreateInitialData.sql";
        var resetScriptFileContents = FileReader.ReadFile(dbInitialDataPath);
        await ExecuteFile(resetScriptFileContents);
    }
    
    private async Task SetupFakeInitialData()
    {
        await _accountHolderAccess.CreateOne(FakeInitialData.SampleAccountHolder1);
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