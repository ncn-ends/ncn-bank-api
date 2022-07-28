CREATE TYPE ReturnType_Checks_DeactivateOneById AS (
    check_id uuid,
    account_number TEXT,
    routing_number TEXT,
    expiration TIMESTAMPTZ,
    deactivated BOOL,
    created_at timestamptz
);

CREATE OR REPLACE FUNCTION SR_Checks_DeactivateOneById(_check_id uuid)
RETURNS setof ReturnType_Checks_DeactivateOneById AS $$
BEGIN
    RETURN QUERY
    WITH check_to_deactivate AS (
        SELECT a.account_id::UUID, a.account_number
        FROM checks
        JOIN accounts a ON checks.account_id = a.account_id
        WHERE checks.check_id = _check_id
    )
    INSERT INTO checks (account_id, deactivated)
    VALUES ((SELECT account_id FROM check_to_deactivate)::UUID, true)
    RETURNING
        checks.check_id,
        (SELECT check_to_deactivate.account_number FROM check_to_deactivate)::text AS account_number,
        checks.routing_number::text,
        checks.expiration,
        checks.deactivated AS deactivated,
        checks.created_at AS created_at;
END;
$$ LANGUAGE plpgsql;