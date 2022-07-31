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

// account_id UUID NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
// account_holder_id UUID NOT NULL REFERENCES account_holders(account_holder_id),
// account_type_id INT NOT NULL REFERENCES account_types(account_type_id),
// routing_number NUMERIC(9, 0) NOT NULL DEFAULT gen_random_number(9),
// account_number NUMERIC(9, 0) NOT NULL DEFAULT gen_random_number(9)
public class AccountDTO
{
    public Guid account_id { get; set; } = Guid.Empty;
    public Guid account_holder_id { get; set; } = Guid.Empty;
    public int account_type_id { get; set; } = -1;
    public long routing_number { get; set; } = -1;
    public long account_number { get; set; } = -1;
}

public class AccountBO
{
    public Guid account_id { get; set; }
    public AccountHolderBO account_holder { get; set; }
    public int account_type_id { get; set; }
    public string routing_number { get; set; }
    public string account_number { get; set; }
}


public class AccountBalanceDTO
{
    public decimal balance { get; set; }
}