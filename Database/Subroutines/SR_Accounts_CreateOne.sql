CREATE TYPE ReturnType_Accounts_CreateOne AS (
    account_id UUID,
    routing_number NUMERIC(9,0),
    account_number NUMERIC(9,0)
);

CREATE OR REPLACE FUNCTION SR_Accounts_CreateOne(
    _account_holder_id UUID,
    _account_type_key TEXT,
    _initial_deposit_amount NUMERIC
)
RETURNS SETOF returntype_accounts_createone AS $$
BEGIN
    RETURN QUERY
    WITH account_type AS (
        SELECT account_type_id
        FROM account_types
        WHERE account_types.name_internal = _account_type_key
    ), account_insertion AS (
        INSERT INTO accounts (account_holder_id, account_type_id, routing_number, account_number)
        VALUES (
            _account_holder_id,
            (SELECT account_type.account_type_id from ACCOUNT_TYPE),
            gen_random_number(9)::numeric(9, 0),
            gen_random_number(9)::numeric(9, 0)
        )
        RETURNING
            accounts.account_id,
            accounts.routing_number,
            accounts.account_number
    ), initial_deposit AS (
        INSERT INTO
            transfers_cash (amount, memo, transfer_target)
        VALUES (
            _initial_deposit_amount,
            'Initial account deposit',
            (SELECT account_id from account_insertion)
        )
    )
    SELECT * FROM account_insertion;
END; $$ LANGUAGE plpgsql;