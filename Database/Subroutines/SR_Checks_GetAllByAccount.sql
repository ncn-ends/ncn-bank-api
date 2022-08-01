CREATE OR REPLACE FUNCTION SR_Checks_GetAllByAccount(_account_id uuid)
RETURNS setof ReturnType_Checks_StandardReturn AS $$
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
    WHERE checks.account_id = _account_id;
END;
$$ LANGUAGE plpgsql;