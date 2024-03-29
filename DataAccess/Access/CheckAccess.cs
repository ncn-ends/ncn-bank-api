using DataAccess.Models;

namespace DataAccess.Access;

public interface ICheckAccess
{
    Task<CheckBO?> CreateOne(CheckCreationForm checkForm);
    Task<CheckBO?> DeactivateOneById(Guid checkId);
    Task<CheckBO?> GetRandomOne();
    Task<IEnumerable<CheckBO>> GetAllByAccount(Guid accountId);
    Task<IEnumerable<CheckBO>> GetAllByAccountHolder(Guid holderId);
}

public class CheckAccess : ICheckAccess
{
    private readonly ISqlDataAccess _dataAccess;

    public CheckAccess(ISqlDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<CheckBO?> CreateOne(CheckCreationForm checkForm)
    {
        var createdCheck = await _dataAccess.CallUdf<CheckBO, dynamic>("SR_Checks_CreateOne", new
        {
            _account_id = checkForm.account_id
        });

        return createdCheck.FirstOrDefault();
    }

    public async Task<CheckBO?> DeactivateOneById(Guid checkId)
    {
        var deactivatedCheck = await _dataAccess.CallUdf<CheckBO, dynamic>("SR_Checks_DeactivateOneById",
            new
            {
                _check_id = checkId
            });

        return deactivatedCheck.FirstOrDefault();
    }

    public async Task<CheckBO?> GetRandomOne()
    {
        var isDev = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        if (!isDev) throw new Exception("Method is only available in Development.");

        var randomCheck = await _dataAccess.CallUdf<CheckBO>("SR_Checks_GetRandomOne");

        return randomCheck.FirstOrDefault();
    }
    
    
    public async Task<IEnumerable<CheckBO>> GetAllByAccount(Guid accountId)
    {
        var allChecks = await _dataAccess.CallUdf<CheckBO, dynamic>("SR_Checks_GetAllByAccount", new
        {
            _account_id = accountId
        });

        return allChecks;
    }

    public async Task<IEnumerable<CheckBO>> GetAllByAccountHolder(Guid holderId)
    {
        var allChecks = await _dataAccess.CallUdf<CheckBO, dynamic>("SR_Checks_GetAllByAccountHolder", new
        {
            _account_holder_id = holderId
        });
        return allChecks;
    }
}