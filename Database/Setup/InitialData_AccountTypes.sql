INSERT INTO account_types(
    name_internal,
    name_display,
    debit_card_access,
    paper_check_access,
--     monthly_fee_id
    overcharge_fee_amount,
    holder_minimum_age,
--     transfer_limit_id
    minimum_balance,
    minimum_initial_deposit,
    withdrawal_penalty_flat,
    withdrawal_penalty_rate,
    apy,
    maturity_term_days
)
VALUES
    ('stu_ca', 'Student Checking Account', true, false, 0.00, 0, 0.00, 100.00, 0.00, 0.00, 0.0000, null),
    ('sta_ca', 'Standard Checking Account', true, true, 35.00, 18, 0.00, 300.00, 0.00, 0.00, 0.0000, null),
    ('sta_sa', 'Standard Savings Account', false, false, null, 18, 300.00, 300.00, 0.00, 0.00, 0.0001, null),
    ('hy_sa', 'High-yield Savings Account', false, false, null, 18, 10000.00, 10000.00, 0.00, 0.00, 0.0025, null),
    ('t1_cd', 'Type 1 Certificate of Deposit (CD)', false, false, null, 18, 1000.00, 1000.00, 100.00, 0.05, 0.0050, 28),
    ('t2_cd', 'Type 2 Certificate of Deposit (CD)', false, false, null, 18, 1000.00, 1000.00, 250.00, 0.05, 0.0070, 56),
    ('t3_cd', 'Type 3 Certificate of Deposit (CD)', false, false, null, 18, 1000.00, 1000.00, 500.00, 0.05, 0.0120, 112)