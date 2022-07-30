CREATE TYPE ReturnType_Checks_GetRandomOne AS (
    check_id uuid,
    account_number TEXT,
    routing_number TEXT,
    expiration TIMESTAMPTZ,
    deactivated BOOL,
    created_at timestamptz
);

CREATE OR REPLACE FUNCTION SR_Checks_GetRandomOne()
RETURNS setof ReturnType_Checks_GetRandomOne AS $$
BEGIN
    RETURN QUERY
    SELECT
        checks.check_id,
        accnt.account_number::text,
        checks.routing_number::text,
        checks.expiration,
        checks.deactivated,
        checks.created_at
    FROM checks
    JOIN
        accounts accnt ON checks.account_id = accnt.account_id
    ORDER BY random()
    LIMIT 1;
END;
$$ LANGUAGE plpgsql;