using System.Data;
using System.Diagnostics;
using Dapper;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Access;

public interface IAccountTypeAccess
{
    Task<IEnumerable<AccountTypeBO>> GetAllAccountTypes();
}

public class AccountTypeAccess : IAccountTypeAccess
{
    private readonly ISqlDataAccess _db;
    private readonly string _connectionString;

    public AccountTypeAccess(ISqlDataAccess db, IConfiguration config)
    {
        _connectionString = config["POSTGRESQLCONNSTR_Default"]
                            ?? config.GetConnectionString("Default");
        _db = db;
    }

    public async Task<IEnumerable<AccountTypeBO>> GetAllAccountTypes()
    {
        using IDbConnection connection = new NpgsqlConnection(_connectionString);

        var result = await connection.QueryAsync<AccountTypeBO, TransferLimitBO, MonthlyFeeBO, AccountTypeBO>(
            "SR_AccountTypes_GetAll",
            commandType: CommandType.StoredProcedure,
            splitOn: "account_type_id,transfer_limit_id,monthly_fee_id",
            map: (accountType, transferLimit, monthlyFee) =>
            {
                
                accountType.monthly_fee = monthlyFee;
                accountType.transfer_limit = transferLimit;
                return accountType;
            }
        );

        return result;
    }
}