using System.Diagnostics;
using DataAccess.Models;

namespace DataAccess.Access;

public interface IAccountHolderAccess
{
    Task<AccountHolderBO?> GetOne(Guid holderId);
    Task<Guid?> CreateOne(AccountHolderForm holderForm);
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

    public async Task<Guid?> CreateOne(AccountHolderForm holderForm)
    {
        var holderCreateUdfRes = await _dataAccess.CallUdf<AccountHolderInsertionResult, dynamic>(
            "sr_accountholders_createone", 
            new
            {
                _birthdate = holderForm.birthdate,
                _firstname = holderForm.firstname, 
                _middlename = holderForm.middlename, 
                _lastname = holderForm.lastname, 
                _phone_number = holderForm.phone_number, 
                _job_title = holderForm.job_title, 
                _expected_salary = holderForm.expected_salary,
                _street = holderForm.street,
                _zipcode = holderForm.zipcode,
                _city = holderForm.city,
                _state = holderForm.state,
                _country = holderForm.country,
                _unit_number = holderForm.unit_number,
                _address_type = holderForm.address_type
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