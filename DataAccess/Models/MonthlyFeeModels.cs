namespace DataAccess.Models;

// monthly_fee_id SERIAL NOT NULL PRIMARY KEY,
// title_internal VARCHAR(10) NOT NULL UNIQUE,
// title_public VARCHAR(50) NOT NULL,
// description VARCHAR(250) NOT NULL DEFAULT '',
// amount_flat MONEY NOT NULL,
// amount_perc NUMERIC(5,4) NOT NULL,
// waived_on_balance_of MONEY

public class MonthlyFeeBO
{
    public int monthly_fee_id { get; set; } = -1;
    public string mf_title_internal { get; set; } = "INTERNAL TITLE UNSET";
    public string mf_title_public { get; set; } = "PUBLIC UNSET TITLE";
    public string mf_description { get; set; } = "";
    public decimal mf_amount_flat { get; set; } = -1;
    public decimal mf_amount_perc { get; set; } = -1;
    public decimal? mf_waved_on_balance_of { get; set; }
}