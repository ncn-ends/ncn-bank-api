namespace DataAccess.Models;

// account_type_id SERIAL PRIMARY KEY,
// name_internal VARCHAR(10) NOT NULL,
// name_display VARCHAR(64) NOT NULL,
// debit_card_access BOOLEAN NOT NULL,
// paper_check_access BOOLEAN NOT NULL,
// monthly_fee_id INT NOT NULL REFERENCES monthly_fees(monthly_fee_id),
// overcharge_fee_amount MONEY,
// holder_minimum_age INT2 NOT NULL,
// transfer_limit_id INT REFERENCES transfer_limits(transfer_limit_id),
// minimum_balance MONEY NOT NULL,
// minimum_initial_deposit MONEY NOT NULL,
// withdrawal_penalty_flat MONEY NOT NULL,
// withdrawal_penalty_rate DECIMAL(7, 6) NOT NULL,
// apy DECIMAL(7, 6) NOT NULL,
// maturity_term_days INT2

public class AccountTypeBO
{
    public int account_type_id { get; set; } = -1;
    public string name_internal { get; set; } = "UNSET INTERNAL NAME";
    public string name_display { get; set; } = "UNSET DISPLAY NAME";
    public bool debit_card_access { get; set; }
    public bool paper_check_access { get; set; }
    public MonthlyFeeBO monthly_fee { get; set; } = new ();
    public decimal? overcharge_fee_amount { get; set; }
    public int holder_minimum_age { get; set; } = -1;
    public TransferLimitBO transfer_limit { get; set; } = new ();
    public decimal minimum_balance { get; set; } = -1;
    public decimal minimum_initial_deposit { get; set; } = -1;
    public decimal withdrawal_penalty_flat { get; set; } = -1;
    public decimal withdrawal_penalty_rate { get; set; } = -1;
    public decimal apy { get; set; } = -1;
    public int? maturity_term_days { get; set; }
}