CREATE OR REPLACE FUNCTION sr_checks_getallbyaccount(
    _account_id uuid
) RETURNS SETOF returntype_checks_standardreturn AS $$
BEGIN
    RETURN QUERY SELECT
        View_ActiveChecks.check_id,
        accnt.account_number::TEXT,
        View_ActiveChecks.routing_number::TEXT,
        View_ActiveChecks.expiration,
        View_ActiveChecks.deactivated,
        View_ActiveChecks.created_at
    FROM
        View_ActiveChecks
        JOIN accounts accnt ON View_ActiveChecks.account_id = accnt.account_id
    WHERE View_ActiveChecks.account_id = _account_id;
END; $$ LANGUAGE plpgsql;