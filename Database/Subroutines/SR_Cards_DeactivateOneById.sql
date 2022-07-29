CREATE TYPE ReturnType_Cards_DeactivateOneById AS (
    card_id uuid,
    card_number TEXT,
    csv TEXT,
    pin_number TEXT,
    expiration TIMESTAMPTZ,
    deactivated BOOL,
    created_at TIMESTAMPTZ
);

CREATE OR REPLACE FUNCTION SR_Cards_DeactivateOneById(_card_id uuid)
RETURNS setof ReturnType_Cards_DeactivateOneById AS $$
BEGIN
    RETURN QUERY
    WITH card_to_deactivate AS (
        SELECT a.account_id, cards.pin_number
        FROM cards
        JOIN accounts a ON a.account_id = cards.account_id
        WHERE cards.card_id = _card_id
    )
    INSERT INTO cards (account_id, pin_number, deactivated)
    VALUES (
        (SELECT account_id FROM card_to_deactivate),
        (SELECT pin_number FROM card_to_deactivate),
        true
    )
    RETURNING
        cards.card_id,
        cards.card_number::TEXT,
        cards.csv::TEXT,
        cards.pin_number::TEXT,
        cards.expiration,
        cards.deactivated,
        cards.created_at;
END;
$$ LANGUAGE plpgsql;