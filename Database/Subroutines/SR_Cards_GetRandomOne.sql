CREATE OR REPLACE FUNCTION SR_Cards_GetRandomOne()
RETURNS setof ReturnType_Cards_StandardReturn AS $$
BEGIN
    RETURN QUERY
    SELECT
        cards.card_id,
        cards.card_number::TEXT,
        cards.pin_number::TEXT,
        cards.csv::text,
        cards.expiration,
        cards.deactivated,
        cards.created_at
    FROM cards
    ORDER BY random()
    LIMIT 1;
END;
$$ LANGUAGE plpgsql;