CREATE OR REPLACE FUNCTION SR_Cards_GetAllByAccount(_account_id uuid)
RETURNS setof ReturnType_Cards_StandardReturn AS $$
BEGIN
    RETURN QUERY
    SELECT
        cards.card_id,
        cards.card_number::TEXT,
        cards.pin_number::TEXT,
        cards.expiration,
        cards.deactivated,
        cards.created_at
    FROM cards
    WHERE cards.account_id = _account_id;
END;
$$ LANGUAGE plpgsql;