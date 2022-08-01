using DataAccess.Models;

namespace DataAccess.Access;

public interface ICardAccess
{
    Task<CardBO?> CreateOne(CardCreationForm cardForm);
    Task<CardBO?> DeactivateOneById(Guid cardId);
    Task<CardBO?> GetRandomOne();
    Task<IEnumerable<CardBO>> GetAllByAccount(Guid accountId);
}

public class CardAccess : ICardAccess
{
    private readonly ISqlDataAccess _dataAccess;

    public CardAccess(ISqlDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<CardBO?> CreateOne(CardCreationForm cardForm)
    {
        var createdCard = await _dataAccess.CallUdf<CardBO, dynamic>("SR_Cards_CreateOne", new
        {
            _account_id = cardForm.account_id,
            _pin_number = cardForm.pin_number
        });

        return createdCard.FirstOrDefault();
    }

    public async Task<CardBO?> DeactivateOneById(Guid cardId)
    {
        var deactivatedCard = await _dataAccess.CallUdf<CardBO, dynamic>("SR_Cards_DeactivateOneById", new
        {
            _card_id = cardId
        });

        return deactivatedCard.FirstOrDefault();
    }

    public async Task<CardBO?> GetRandomOne()
    {
        var isDev = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        if (!isDev) throw new Exception("Method is only available in Development.");

        var randomCard = await _dataAccess.CallUdf<CardBO>("SR_Cards_GetRandomOne");

        return randomCard.FirstOrDefault();
    }

    public async Task<IEnumerable<CardBO>> GetAllByAccount(Guid accountId)
    {
        var cards = await _dataAccess.CallUdf<CardBO, dynamic>("SR_Cards_GetAllByAccount", new
        {
            _account_id = accountId
        });

        return cards;
    }
}