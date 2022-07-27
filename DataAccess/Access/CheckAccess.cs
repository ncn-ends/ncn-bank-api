using DataAccess.Models;

namespace DataAccess.Access;

public interface ICheckAccess
{
    Task<CheckInsertionReturn?> CreateOne(CheckCreationForm checkForm);
}

public class CheckAccess : ICheckAccess
{
    private readonly ISqlDataAccess _dataAccess;

    public CheckAccess(ISqlDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<CheckInsertionReturn?> CreateOne(CheckCreationForm checkForm)
    {
        var createdCheck = await _dataAccess.CallUdf<CheckInsertionReturn, dynamic>("SR_Checks_CreateOne", new
        {
            _account_id = checkForm.account_id
        });

        return createdCheck.FirstOrDefault();
    }
}