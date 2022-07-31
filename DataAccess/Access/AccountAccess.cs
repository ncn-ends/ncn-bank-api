using System.Data;
using System.Diagnostics;
using Dapper;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.Access;

public interface IAccountAccess
{
    Task<AccountInsertionReturn?> CreateOne(AccountFormDTO accountForm);
    Task<AccountDTO?> GetOneById(Guid accountId);
    Task<AccountDTO?> GetRandomOne();
    Task<AccountBO?> SearchByHolderName(string name);
    Task<AccountBalanceDTO?> GetAccountBalance(Guid accountId);
}

public class AccountAccess : IAccountAccess
{
    private readonly ISqlDataAccess _dataAccess;
    private readonly string _connectionString;

    public AccountAccess(ISqlDataAccess dataAccess, IConfiguration config)
    {
        _connectionString = config["POSTGRESQLCONNSTR_Default"]
                            ?? config.GetConnectionString("Default");
        
        _dataAccess = dataAccess;
    }

    public async Task<AccountInsertionReturn?> CreateOne(AccountFormDTO accountForm)
    {
        // toDO: IMPLEMENT database code for initial deposit once creating transfer table
        var createdAccount = await _dataAccess.CallUdf<AccountInsertionReturn, dynamic>("SR_Accounts_CreateOne", new
        {
            _account_holder_id = accountForm.account_holder_id,
            _account_type_key = accountForm.account_type_key,
            _initial_deposit = accountForm.initial_deposit
        });
        return createdAccount.FirstOrDefault();
    }

    public async Task<AccountDTO?> GetOneById(Guid accountId)
    {
        var fetchedAccount = await _dataAccess.CallUdf<AccountDTO, dynamic>("SR_Accounts_GetOne", new
        {
            _account_id = accountId
        });

        return fetchedAccount.FirstOrDefault();
    }
    
    // TODO: untested
    public async Task<AccountDTO?> GetRandomOne()
    {
        var fetchedAccount = await _dataAccess.CallUdf<AccountDTO>("SR_Accounts_GetRandomOne");

        return fetchedAccount.FirstOrDefault();
    }
    
    public async Task<AccountBO?> SearchByHolderName(string name)
    {
        using IDbConnection connection = new NpgsqlConnection(_connectionString);

        var result = await connection.QueryAsync<AccountBO, AccountHolderBO, AccountBO>(
            "SR_Accounts_SearchByHolderName",
            commandType: CommandType.StoredProcedure,
            splitOn: "account_id,account_holder_id",
            param: new
            {
                _name = name  
            },
            map: (account, accountHolder) =>
            {
                account.account_holder = accountHolder;
                return account;
            }
        );

        return result.FirstOrDefault();
    }

    public async Task<AccountBalanceDTO?> GetAccountBalance(Guid accountId)
    {
        var accountBalance = await _dataAccess.CallUdf<AccountBalanceDTO, dynamic>("SR_Accounts_GetAccountBalance", new
        {
            _account_id = accountId
        });
        
        return accountBalance.FirstOrDefault();
    }
}