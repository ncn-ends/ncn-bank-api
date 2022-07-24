CREATE TYPE ReturnType_AccountTypes_GetAll AS (
    account_type_id int,
    name_internal varchar(10),
    name_display varchar(64),
	debit_card_access boolean,
	paper_check_access boolean,
	overcharge_fee_amount money,
	holder_minimum_age smallint,
	minimum_balance money,
	minimum_initial_deposit money,
	withdrawal_penalty_flat money,
	withdrawal_penalty_rate numeric(7,6),
	apy numeric(7,6),
	maturity_term_days smallint,
    transfer_limit_id int,
	tl_single_flat money,
	tl_cycle_flat money,
	tl_cycle_count integer,
    monthly_fee_id int,
    mf_title_internal varchar(10),
	mf_title_public varchar(50),
	mf_description varchar(250),
	mf_amount_flat money,
	mf_amount_perc numeric(5,4),
	mf_waived_on_balance_of money
);

CREATE OR REPLACE FUNCTION SR_AccountTypes_GetAll()
RETURNS setof ReturnType_AccountTypes_GetAll AS $$
BEGIN
    RETURN QUERY
    SELECT
        account_type_id,
        name_internal,
        name_display,
        debit_card_access,
        paper_check_access,
        overcharge_fee_amount,
        holder_minimum_age,
        minimum_balance,
        minimum_initial_deposit,
        withdrawal_penalty_flat,
        withdrawal_penalty_rate,
        apy,
        maturity_term_days,
        tl.transfer_limit_id,
        tl.single_flat AS tl_single_flat,
        tl.cycle_flat AS tl_cycle_flat,
        tl.cycle_count AS tl_cycle_count,
        mf.monthly_fee_id,
        mf.title_internal AS mf_title_internal,
        mf.title_public AS mf_title_public,
        mf.description AS mf_description,
        mf.amount_flat AS mf_amount_flat,
        mf.amount_perc AS mf_account_perc,
        mf.waived_on_balance_of AS mf_waived_on_balance_of
    FROM account_types
    LEFT JOIN transfer_limits tl ON account_types.transfer_limit_id = tl.transfer_limit_id
    LEFT JOIN monthly_fees mf ON account_types.monthly_fee_id = mf.monthly_fee_id;
END; $$ LANGUAGE plpgsql;