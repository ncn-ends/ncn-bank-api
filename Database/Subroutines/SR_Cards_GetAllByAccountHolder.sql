CREATE OR REPLACE FUNCTION SR_Cards_GetAllByAccountHolder(_account_holder_id uuid)
RETURNS setof ReturnType_Cards_StandardReturn AS $$
BEGIN
    RETURN QUERY
    SELECT
        cards.card_id,
        cards.card_number::TEXT,
        cards.pin_number::TEXT,
        cards.csv::TEXT,
        cards.expiration,
        cards.deactivated,
        cards.created_at
    FROM cards
    JOIN accounts a ON cards.account_id = a.account_id
    JOIN account_holders ah ON a.account_holder_id = ah.account_holder_id
    WHERE ah.account_holder_id = _account_holder_id;
END;
$$ LANGUAGE plpgsql;