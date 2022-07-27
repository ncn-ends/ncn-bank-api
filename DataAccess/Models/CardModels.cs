namespace DataAccess.Models;

// card_id uuid,
// card_number TEXT,
// csv TEXT,
// pin_number TEXT,
// expiration TIMESTAMPTZ
public class CardInsertionReturn
{
    public Guid card_id { get; set; } = Guid.Empty;
    public string card_number { get; set; } = "UNSET CARD NUMBER";
    public string csv { get; set; } = "UNSET CSV";
    public string pin_number { get; set; } = "UNSET PIN NUMBER";
    public DateTime expiration { get; set; } = DateTime.Now;
}

public class CardCreationForm
{
    public Guid account_id { get; set; }
    public string pin_number { get; set; }
}