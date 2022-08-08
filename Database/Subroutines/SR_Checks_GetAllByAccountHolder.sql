CREATE OR REPLACE FUNCTION sr_checks_getallbyaccountholder(
    _account_holder_id uuid
) RETURNS SETOF returntype_checks_standardreturn AS $$
BEGIN
    RETURN QUERY SELECT
        View_ActiveChecks.check_id,
        a.account_number::TEXT,
        View_ActiveChecks.routing_number::TEXT,
        View_ActiveChecks.expiration,
        View_ActiveChecks.deactivated,
        View_ActiveChecks.created_at
    FROM View_ActiveChecks
    JOIN accounts a ON View_ActiveChecks.account_id = a.account_id
    JOIN account_holders ah ON a.account_holder_id = ah.account_holder_id
    WHERE ah.account_holder_id = _account_holder_id;
END; $$ LANGUAGE plpgsql;