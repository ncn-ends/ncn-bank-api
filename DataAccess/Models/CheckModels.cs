using System.Security.AccessControl;

namespace DataAccess.Models;

public class CheckCreationForm
{
    public Guid account_id { get; set; } = Guid.Empty;
}


// check_id uuid,
// account_number TEXT,
// routing_number TEXT,
// expiration TIMESTAMPTZ
public class CheckInsertionReturn
{
    public Guid check_id { get; set; }
    public string? account_number { get; set; }
    public string? routing_number { get; set; }
    public DateTime? expiration { get; set; }
}