CREATE OR REPLACE FUNCTION SR_Checks_DeactivateOneById(_check_id uuid)
RETURNS setof returntype_checks_standardreturn AS $$
BEGIN
    RETURN QUERY
    WITH check_to_deactivate AS (
        SELECT a.account_id::UUID, a.account_number, checks.check_id
        FROM checks
        JOIN accounts a ON checks.account_id = a.account_id
        WHERE checks.check_id = _check_id
    )
    INSERT INTO checks (account_id, deactivated)
    VALUES (
        (SELECT account_id FROM check_to_deactivate)::UUID,
        (SELECT check_id FROM check_to_deactivate)
    )
    RETURNING
        checks.check_id,
        (SELECT check_to_deactivate.account_number FROM check_to_deactivate)::text AS account_number,
        checks.routing_number::text,
        checks.expiration,
        checks.deactivated,
        checks.created_at;
END;
$$ LANGUAGE plpgsql;