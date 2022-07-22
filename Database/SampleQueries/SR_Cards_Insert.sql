CREATE OR REPLACE PROCEDURE sr_cards_insert(
    _account_id UUID,
    _pin_number NUMERIC(4, 0)
)
LANGUAGE plpgsql AS $$
BEGIN
    INSERT INTO cards (account_id, pin_number, expiration)
    VALUES (_account_id, _pin_number, now() + interval '54 month');
END;$$