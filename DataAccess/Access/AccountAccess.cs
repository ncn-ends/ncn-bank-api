using System.Diagnostics;
using DataAccess.Models;

namespace DataAccess.Access;

public interface IAccountAccess
{
    Task<AccountInsertionReturn?> CreateOne(AccountFormDTO accountForm);
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
        var createdAccount = await _dataAccess.CallUdf<AccountInsertionReturn, dynamic>("SR_Accounts_CreateOne", new
        {
            _account_holder_id = accountForm.account_holder_id,
            _account_type_key = accountForm.account_type_key,
            _initial_deposit = accountForm.initial_deposit
        });
        return createdAccount.FirstOrDefault();
    }
}