CREATE OR REPLACE PROCEDURE SR_InitialData_AccountTypes()
AS $$
DECLARE
    has_row bool := (SELECT exists(SELECT 1 from account_types) as has_row);
BEGIN
    IF has_row THEN RETURN;
    ELSE
        INSERT INTO monthly_fees (
            title_internal,
            title_public,
            description,
            amount_flat,
            amount_perc,
            waived_on_balance_of )
        VALUES
            ('mmf_non', 'Monthly Maintenance Fee', '', 0, 0, null),
            ('mmf_sta', 'Monthly Maintenance Fee', '', 15, 0, 300),
            ('mmf_sav', 'Monthly Maintenance Fee', '', 10, 0, 1000);

        INSERT INTO transfer_limits(
            single_flat,
            cycle_flat,
            cycle_count)
        VALUES
            (null, null, 6),
            (null, null, 2),
            (null, null, 0);

        INSERT INTO account_types(
            name_internal,
            name_display,
            debit_card_access,
            paper_check_access,
            monthly_fee_id,
            overcharge_fee_amount,
            holder_minimum_age,
            transfer_limit_id,
            minimum_balance,
            minimum_initial_deposit,
            withdrawal_penalty_flat,
            withdrawal_penalty_rate,
            apy,
            maturity_term_days
        )
        VALUES
            ('stu_ca', 'Student Checking Account', true, false, 1, 0.00, 0, null, 0.00, 100.00, 0.00, 0.00, 0.0000, null),
            ('sta_ca', 'Standard Checking Account', true, true, 2, 35.00, 18, null, 0.00, 300.00, 0.00, 0.00, 0.0000, null),
            ('sta_sa', 'Standard Savings Account', false, false, 3, null, 18, 1, 300.00, 300.00, 0.00, 0.00, 0.0001, null),
            ('hy_sa', 'High-yield Savings Account', false, false, 1, null, 18, 2, 10000.00, 10000.00, 0.00, 0.00, 0.0025, null),
            ('t1_cd', 'Type 1 Certificate of Deposit (CD)', false, false, 1, null, 18, 3, 1000.00, 1000.00, 100.00, 0.05, 0.0050, 28),
            ('t2_cd', 'Type 2 Certificate of Deposit (CD)', false, false, 1, null, 18, 3, 1000.00, 1000.00, 250.00, 0.05, 0.0070, 56),
            ('t3_cd', 'Type 3 Certificate of Deposit (CD)', false, false, 1, null, 18, 3, 1000.00, 1000.00, 500.00, 0.05, 0.0120, 112);
    END IF;
END; $$LANGUAGE plpgsql;