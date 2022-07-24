namespace DataAccess.Models;
// transfer_limit_id SERIAL NOT NULL PRIMARY KEY,
// single_flat MONEY,
// cycle_flat MONEY,
// cycle_count INT

public class TransferLimitBO
{
    public int transfer_limit_id { get; set; } = -1;
    public decimal? tl_single_flat { get; set; }
    public decimal? tl_cycle_flat { get; set; }
    public int? tl_cycle_count { get; set; }
}