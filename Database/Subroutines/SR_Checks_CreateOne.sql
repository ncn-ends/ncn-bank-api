CREATE OR REPLACE FUNCTION SR_Checks_CreateOne(_account_id uuid)
RETURNS setof returntype_checks_standardreturn AS $$
BEGIN
    RETURN QUERY
    WITH account AS (
        SELECT account_number
        FROM accounts
        WHERE accounts.account_id = _account_id
    )
    INSERT INTO checks (account_id)
    VALUES (_account_id)
    RETURNING
        checks.check_id,
        (SELECT * FROM account)::text AS account_number,
        checks.routing_number::text,
        checks.expiration,
        checks.deactivated,
        checks.created_at;
END;
$$ LANGUAGE plpgsql;