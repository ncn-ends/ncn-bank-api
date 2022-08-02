CREATE OR REPLACE FUNCTION sr_checks_getallbyaccount(
    _account_id uuid
) RETURNS SETOF returntype_checks_standardreturn AS $$
BEGIN
    RETURN QUERY SELECT
        checks.check_id,
        accnt.account_number::TEXT,
        checks.routing_number::TEXT,
        checks.expiration,
        checks.deactivated,
        checks.created_at
    FROM
        checks
        JOIN accounts accnt ON checks.account_id = accnt.account_id
    WHERE checks.account_id = _account_id;
END; $$ LANGUAGE plpgsql;