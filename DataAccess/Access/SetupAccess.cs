namespace DataAccess.Access;

public interface ISetupAccess
{
    Task EnsureDatabaseSetup();
    Task SetupTables();
}

public class SetupAccess : ISetupAccess
{
    private readonly ISqlDataAccess _dataAccess;

    public SetupAccess(ISqlDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task EnsureDatabaseSetup()
    {
        await SetupTables();
        
    }

    public async Task SetupTables()
    {
        await _dataAccess.ExecRawSql("CALL SR_CreateTable_transfer_limits();");
        await _dataAccess.ExecRawSql("CALL SR_CreateTable_monthly_fees();");
        await _dataAccess.ExecRawSql("CALL SR_CreateTable_account_types();");
        await _dataAccess.ExecRawSql("CALL SR_CreateTable_account_holders();");
        await _dataAccess.ExecRawSql("CALL SR_CreateTable_accounts();");
        await _dataAccess.ExecRawSql("CALL SR_CreateTable_addresses();");
        await _dataAccess.ExecRawSql("CALL SR_CreateTable_cards();");
        await _dataAccess.ExecRawSql("CALL SR_CreateTable_checks();");
        await _dataAccess.ExecRawSql("CALL SR_CreateTable_transfers();");
        await _dataAccess.ExecRawSql("CALL SR_CreateTable_account_links();");
        await _dataAccess.ExecRawSql("CALL SR_CreateTable_branches();");
    }
}