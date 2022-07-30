namespace DataAccess.Models;


// transfers_check.transfer_id,
// transfers_check.amount,
// transfers_check.memo;
public class TransferInsertionReturn
{
    public Guid transfer_id { get; set; }
    public decimal amount { get; set; }
    public string memo { get; set; }
}

// _amount money, _routing_number text, _transfer_target uuid, _memo text
public class CheckTransferForm
{
    public decimal amount { get; set; }
    public string routing_number { get; set; }
    public Guid transfer_target { get; set; }
    public string memo { get; set; } = "";
}

// _amount numeric, _card_number text, _transfer_target uuid, _memo text
public class CardTransferForm
{
    public decimal amount { get; set; }
    public string card_number { get; set; }
    public Guid transfer_target { get; set; }
    public string memo { get; set; } = "";
}