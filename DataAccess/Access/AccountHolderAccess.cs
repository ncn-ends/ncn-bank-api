using System.Diagnostics;
using DataAccess.Models;

namespace DataAccess.Access;

public interface IAccountHolderAccess
{
    Task<AccountHolderBO?> GetOne(Guid holderId);
    Task<Guid?> CreateOne(AccountHolderDTO holderDto);
    Task<AccountHolderBO?> GetRandomOne();
    Task<AccountBalanceDTO?> GetBalance(Guid holderId);
}

public class AccountHolderAccess : IAccountHolderAccess
{
    private readonly ISqlDataAccess _dataAccess;

    public AccountHolderAccess(ISqlDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<AccountHolderBO?> GetOne(Guid holderId)
    {
        var holder = await _dataAccess.CallUdf<AccountHolderBO, dynamic>("sr_accountholders_getone", new
        {
            _account_holder_id = holderId
        });
        return holder.FirstOrDefault();
    }

    public async Task<Guid?> CreateOne(AccountHolderDTO holderDto)
    {
        var holderCreateUdfRes = await _dataAccess.CallUdf<AccountHolderInsertionResult, dynamic>(
            "sr_accountholders_createone", 
            new
            {
                _birthdate = holderDto.birthdate,
                _firstname = holderDto.firstname, 
                _middlename = holderDto.middlename, 
                _lastname = holderDto.lastname, 
                _phone_number = holderDto.phone_number, 
                _job_title = holderDto.job_title, 
                _expected_salary = holderDto.expected_salary
            });

        var first = holderCreateUdfRes.FirstOrDefault();
        if (first is null) return null;
        return first.account_holder_id;
    }

    // TODO: untested
    public async Task<AccountHolderBO?> GetRandomOne()
    {
        var randomHolder = await _dataAccess.CallUdf<AccountHolderBO>("sr_accountholders_getrandomone");
        return randomHolder.FirstOrDefault();
    }

    public async Task<AccountBalanceDTO?> GetBalance(Guid holderId)
    {
        var overallBalance = await _dataAccess.CallUdf<AccountBalanceDTO, dynamic>("SR_AccountHolders_GetOverallBalance", new
        {
            _holder_id = holderId
        });

        return overallBalance.FirstOrDefault();
    }
}