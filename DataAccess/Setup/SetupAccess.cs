using System.Diagnostics;
using DataAccess.Access;
using DataAccess.Models;
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
    private readonly IAccountAccess _accountAccess;
    private readonly ICheckAccess _checkAccess;
    private readonly ICardAccess _cardAccess;

    public SetupAccess(ISqlDataAccess dataAccess, 
        IAccountHolderAccess accountHolderAccess,
        IAccountAccess accountAccess,
        ICheckAccess checkAccess,
        ICardAccess cardAccess)
    {
        _dataAccess = dataAccess;
        _accountHolderAccess = accountHolderAccess;
        _accountAccess = accountAccess;
        _checkAccess = checkAccess;
        _cardAccess = cardAccess;
    }

    public async Task EnsureDatabaseSetup()
    {
        var isDevEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        // if (isDevEnv)
        // {
            await DatabaseDropAll();
        // }
        
        await SetupCustomTypes();
        await SetupUtilities();
        await SetupTables();
        await SetupViews();
        await SetupSubroutines();
        await SetupRequiredInitialData();

        // if (isDevEnv)
        // {
            await SetupFakeInitialData();
        // }
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
        await ExecuteFiles(files, delay: new[]
        {
            "Addresses_StandardReturn"
        });
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
            "addresses",
            "accounts",
            "cards",
            "checks",
            "transfers",
            "account_links",
            "branches"
        };
        
        var files = FileReader.ReadDirectory(dbTablesPath);
        
        await ExecuteFiles(files, executionOrder: executionOrder);
    }
    
    
    private async Task SetupViews()
    {
        var dbViewsPath = "Database/Views";
        var files = FileReader.ReadDirectory(dbViewsPath);
        await ExecuteFiles(files);
    }

    private async Task SetupRequiredInitialData()
    {
        var dbInitialDataPath = "Database/Setup/CreateInitialData.sql";
        var resetScriptFileContents = FileReader.ReadFile(dbInitialDataPath);
        await ExecuteFile(resetScriptFileContents);
    }
    
    private async Task SetupFakeInitialData()
    {
        var exceptionMsgTemplate = new Func<string, object>((model) =>
            throw new Exception($"When creating fake initial data for {model}, it returned null"));

        var holderId = await _accountHolderAccess.CreateOne(FakeInitialData.SampleAccountHolder1);
        if (holderId is null) exceptionMsgTemplate("account_holder");
        
        var fakeAccount = await _accountAccess.CreateOne(FakeInitialData.SampleAccount1(holderId.Value));
        if (fakeAccount is null) exceptionMsgTemplate("account");

        var fakeCheck = await _checkAccess.CreateOne(new CheckCreationForm
        {
            account_id = fakeAccount.account_id
        });
        if (fakeCheck is null) exceptionMsgTemplate("check");
        
        
        var fakeCard = await _cardAccess.CreateOne(new CardCreationForm
        {
            account_id = fakeAccount.account_id,
            pin_number = "1234"
        });
        if (fakeCard is null) exceptionMsgTemplate("card");

        var extraFakeHolderId = await _accountHolderAccess.CreateOne(FakeInitialData.SampleAccountHolder2);
        await _accountAccess.CreateOne(FakeInitialData.SampleAccount2(extraFakeHolderId.Value));
    }


    private async Task ExecuteFiles(
        IEnumerable<FileData> files, 
        string[] delay = null
    )
    {
        files = files.ToArray();
        List<FileData> delayedFiles = new List<FileData>();
        
        foreach (var file in files)
        {
            if (delay is not null)
            {
                var fileIsToBeDelayed = delay.Any(el => file.FileName.Contains(el));
                if (fileIsToBeDelayed)
                {
                    delayedFiles.Add(file);
                    continue;
                }
            }
            
            await ExecuteFileFromPath(file.FilePath);
        }
        
        foreach (var delayedFile in delayedFiles)
        {
            await ExecuteFileFromPath(delayedFile.FilePath);
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