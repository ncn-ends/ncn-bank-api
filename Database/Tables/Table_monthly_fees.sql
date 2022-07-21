CREATE OR REPLACE PROCEDURE SR_CreateTable_monthly_fees() AS $$
BEGIN
    CREATE TABLE monthly_fees (
        monthly_fee_id SERIAL NOT NULL PRIMARY KEY,
        title_internal VARCHAR(10) NOT NULL UNIQUE,
        title_public VARCHAR(50) NOT NULL,
        description VARCHAR(250) NOT NULL DEFAULT '',
        amount_flat MONEY NOT NULL,
        amount_perc NUMERIC(5,4) NOT NULL,
        waived_on_balance_of MONEY
    );
END;
$$ LANGUAGE plpgsql;

CALL SR_CreateTable_monthly_fees();