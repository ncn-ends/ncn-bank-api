using DataAccess.Models;

namespace DataAccess.Access;

public interface ICardAccess
{
    Task<CardInsertionReturn?> CreateOne(CardCreationForm cardForm);
    Task<CardInsertionReturn?> DeactivateOneById(Guid cardId);
}

public class CardAccess : ICardAccess
{
    private readonly ISqlDataAccess _dataAccess;

    public CardAccess(ISqlDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<CardInsertionReturn?> CreateOne(CardCreationForm cardForm)
    {
        var createdCard = await _dataAccess.CallUdf<CardInsertionReturn, dynamic>("SR_Cards_CreateOne", new
        {
            _account_id = cardForm.account_id,
            _pin_number = cardForm.pin_number
        });

        return createdCard.FirstOrDefault();
    }

    public async Task<CardInsertionReturn?> DeactivateOneById(Guid cardId)
    {
        var deactivatedCard = await _dataAccess.CallUdf<CardInsertionReturn, dynamic>("SR_Cards_DeactivateOneById", new
        {
            _card_id = cardId
        });

        return deactivatedCard.FirstOrDefault();
    }
}