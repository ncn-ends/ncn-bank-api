using DataAccess.Models;

namespace DataAccess.Access;

public interface ITransferAccess
{
    Task<TransferInsertionReturn?> MakeTransfer(CheckTransferForm transferForm);
    Task<TransferInsertionReturn?> MakeTransfer(CardTransferForm transferForm);
}

public class TransferAccess : ITransferAccess
{
    private readonly ISqlDataAccess _dataAccess;

    public TransferAccess(ISqlDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<TransferInsertionReturn?> MakeTransfer(CheckTransferForm transferForm)
    {
        var transferMade = await _dataAccess.CallUdf<TransferInsertionReturn, dynamic>("SR_Transfers_MakeCheckTransfer", new
        {
            _amount = transferForm.amount, 
            _routing_number = transferForm.routing_number, 
            _transfer_target = transferForm.transfer_target, 
            _memo = transferForm.memo
        });

        return transferMade.FirstOrDefault();
    }

    public async Task<TransferInsertionReturn?> MakeTransfer(CardTransferForm transferForm)
    {
        var transferMade = await _dataAccess.CallUdf<TransferInsertionReturn, dynamic>("SR_Transfers_MakeCardTransfer", new
        {
            _amount = transferForm.amount, 
            _card_number = transferForm.card_number, 
            _transfer_target = transferForm.transfer_target, 
            _memo = transferForm.memo
        });

        return transferMade.FirstOrDefault();
    }
}