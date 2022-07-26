CREATE TYPE ReturnType_Accounts_CreateOne AS (
    account_id UUID,
    routing_number NUMERIC(9,0),
    account_number NUMERIC(9,0)
);

CREATE OR REPLACE FUNCTION SR_Accounts_CreateOne(
    _account_holder_id UUID,
    _account_type_key TEXT,
    _initial_deposit NUMERIC
)
RETURNS SETOF returntype_accounts_createone AS $$
BEGIN
    RETURN QUERY
    WITH account_type AS (
        SELECT account_type_id
        FROM account_types
        WHERE account_types.name_internal = _account_type_key
    )
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
        accounts.account_number;
END;
$$ LANGUAGE plpgsql;

-- SELECT * FROM sr_accounts_createone(
--     '4f62b27c-379a-4152-99b1-2887f26bd626',
--     'stu_ca',
--     1000000
-- );

-- SELECT * FROM accounts;

SELECT * FROM account_holders;