CREATE TYPE ReturnType_Cards_CreateOne AS (
    card_id uuid,
    card_number TEXT,
    csv TEXT,
    pin_number TEXT,
    expiration TIMESTAMPTZ
);

CREATE OR REPLACE FUNCTION SR_Cards_CreateOne(_account_id uuid, _pin_number TEXT)
RETURNS setof ReturnType_Cards_CreateOne AS $$
BEGIN
    RETURN QUERY
    INSERT INTO cards (account_id, pin_number)
    VALUES
        (_account_id, _pin_number::numeric(4,0))
    RETURNING
        cards.card_id,
        cards.card_number::TEXT,
        cards.csv::TEXT,
        cards.pin_number::TEXT,
        cards.expiration;
END;
$$ LANGUAGE plpgsql;