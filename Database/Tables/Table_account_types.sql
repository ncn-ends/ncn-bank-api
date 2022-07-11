CREATE TABLE account_types (
    account_type_id SERIAL PRIMARY KEY,
    name_internal VARCHAR(10) NOT NULL,
    name_display VARCHAR(64) NOT NULL,
    debit_card_access BOOLEAN NOT NULL,
    paper_check_access BOOLEAN NOT NULL,
    monthly_fee_id INT NOT NULL REFERENCES monthly_fees(monthly_fee_id),
    overcharge_fee_amount MONEY,
    holder_minimum_age INT2 NOT NULL,
    transfer_limit_id INT REFERENCES transfer_limits(transfer_limit_id),
    minimum_balance MONEY NOT NULL,
    minimum_initial_deposit MONEY NOT NULL,
    withdrawal_penalty_flat MONEY NOT NULL,
    withdrawal_penalty_rate DECIMAL(7, 6) NOT NULL,
    apy DECIMAL(7, 6) NOT NULL,
    maturity_term_days INT2
)