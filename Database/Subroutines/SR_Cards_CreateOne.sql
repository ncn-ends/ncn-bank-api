CREATE OR REPLACE FUNCTION SR_Cards_CreateOne(_account_id uuid, _pin_number TEXT)
RETURNS setof returntype_cards_standardreturn AS $$
BEGIN
    RETURN QUERY
    INSERT INTO cards (account_id, pin_number)
    VALUES
        (_account_id, _pin_number::numeric(4,0))
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