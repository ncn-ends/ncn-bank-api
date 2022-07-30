CREATE TYPE ReturnType_Cards_GetRandomOne AS (
    check_id uuid,
    card_number TEXT,
    pin_number TEXT,
    expiration TIMESTAMPTZ,
    deactivated BOOL,
    created_at timestamptz
);

CREATE OR REPLACE FUNCTION SR_Cards_GetRandomOne()
RETURNS setof ReturnType_Cards_GetRandomOne AS $$
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
    ORDER BY random()
    LIMIT 1;
END;
$$ LANGUAGE plpgsql;