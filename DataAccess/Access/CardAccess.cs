using DataAccess.Models;

namespace DataAccess.Access;

public interface ICardAccess
{
    Task<CardInsertionReturn?> CreateOne(CardCreationForm cardForm);
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
}