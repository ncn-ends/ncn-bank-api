namespace DataAccess.Models;

public class AccountFormDTO
{
    public Guid account_holder_id { get; set; }
    public string account_type_key { get; set; }
    public decimal initial_deposit { get; set; }
}

// account_id UUID,
// routing_number NUMERIC(9,0),
// account_number NUMERIC(9, 0)
public class AccountInsertionReturn
{
    public Guid account_id { get; set; } = Guid.Empty;
    public long routing_number { get; set; } = -1;
    public long account_number { get; set; } = -1;
}