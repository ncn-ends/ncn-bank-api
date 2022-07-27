using System.Diagnostics;
using DataAccess.Models;

namespace DataAccess.Access;

public interface IAccountAccess
{
    Task<AccountInsertionReturn?> CreateOne(AccountFormDTO accountForm);
    Task<AccountBO?> GetOneById(Guid accountId);
    Task<AccountBO?> GetRandomOne();
}

public class AccountAccess : IAccountAccess
{
    private readonly ISqlDataAccess _dataAccess;

    public AccountAccess(ISqlDataAccess dataAccess)
    {
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

    public async Task<AccountBO?> GetOneById(Guid accountId)
    {
        var fetchedAccount = await _dataAccess.CallUdf<AccountBO, dynamic>("SR_Accounts_GetOne", new
        {
            _account_id = accountId
        });

        return fetchedAccount.FirstOrDefault();
    }
    
    // TODO: untested
    public async Task<AccountBO?> GetRandomOne()
    {
        var fetchedAccount = await _dataAccess.CallUdf<AccountBO>("SR_Accounts_GetRandomOne");

        return fetchedAccount.FirstOrDefault();
    }
}