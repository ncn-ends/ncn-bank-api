using DataAccess.Models;

namespace DataAccess.Access;

public interface ICheckAccess
{
    Task<CheckInsertionReturn?> CreateOne(CheckCreationForm checkForm);
    Task<CheckInsertionReturn?> DeactivateOneById(Guid checkId);
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

    public async Task<CheckInsertionReturn?> DeactivateOneById(Guid checkId)
    {
        var deactivatedCheck = await _dataAccess.CallUdf<CheckInsertionReturn, dynamic>("SR_Checks_DeactivateOneById",
            new
            {
                _check_id = checkId
            });

        return deactivatedCheck.FirstOrDefault();
    }
}