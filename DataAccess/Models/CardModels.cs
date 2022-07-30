namespace DataAccess.Models;

// card_id uuid,
// card_number TEXT,
// csv TEXT,
// pin_number TEXT,
// expiration TIMESTAMPTZ
public class CardBO
{
    public Guid card_id { get; set; } = Guid.Empty;
    public string? card_number { get; set; }
    public string? csv { get; set; }
    public string? pin_number { get; set; }
    public DateTime expiration { get; set; } = DateTime.Now;
    public bool deactivated { get; set; }
    public DateTime? created_at { get; set; }
}

public class CardCreationForm
{
    public Guid account_id { get; set; }
    public string pin_number { get; set; }
}