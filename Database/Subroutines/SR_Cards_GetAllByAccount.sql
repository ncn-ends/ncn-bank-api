CREATE OR REPLACE FUNCTION SR_Cards_GetAllByAccount(_account_id uuid)
RETURNS setof ReturnType_Cards_StandardReturn AS $$
BEGIN
    RETURN QUERY
    SELECT
        card_id,
        card_number::TEXT,
        pin_number::TEXT,
        csv::TEXT,
        expiration,
        deactivated,
        created_at
    FROM view_activecards
    WHERE account_id = _account_id;
END;
$$ LANGUAGE plpgsql;