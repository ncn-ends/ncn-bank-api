using System.Diagnostics;
using DataAccess.Models;

namespace DataAccess.Access;

public interface IAccountHolderAccess
{
    Task<AccountHolderBO?> GetOne(Guid holderId);
    Task<Guid?> CreateOne(AccountHolderDTO holderDto);
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
}