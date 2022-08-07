CREATE OR REPLACE FUNCTION SR_Cards_DeactivateOneById(_card_id uuid)
RETURNS setof returntype_cards_standardreturn AS $$
BEGIN
    RETURN QUERY
    WITH card_to_deactivate AS (
        SELECT
            a.account_id,
            cards.pin_number,
            cards.card_id
        FROM cards
        JOIN accounts a ON a.account_id = cards.account_id
        WHERE cards.card_id = _card_id
    )
    INSERT INTO cards (account_id, pin_number, deactivated)
    VALUES (
        (SELECT account_id FROM card_to_deactivate),
        (SELECT pin_number FROM card_to_deactivate),
        (SELECT card_id FROM card_to_deactivate)
    )
    RETURNING
        cards.card_id,
        cards.card_number::TEXT,
        cards.pin_number::TEXT,
        cards.csv::TEXT,
        cards.expiration,
        cards.deactivated,
        cards.created_at;
END;
$$ LANGUAGE plpgsql;