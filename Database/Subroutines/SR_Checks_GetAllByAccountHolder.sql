CREATE OR REPLACE FUNCTION sr_checks_getallbyaccountholder(
    _account_holder_id uuid
) RETURNS SETOF returntype_checks_standardreturn AS $$
BEGIN
    RETURN QUERY SELECT
        checks.check_id,
        a.account_number::TEXT,
        checks.routing_number::TEXT,
        checks.expiration,
        checks.deactivated,
        checks.created_at
    FROM checks
    JOIN accounts a ON checks.account_id = a.account_id
    JOIN account_holders ah ON a.account_holder_id = ah.account_holder_id
    WHERE ah.account_holder_id = _account_holder_id;
END; $$ LANGUAGE plpgsql;