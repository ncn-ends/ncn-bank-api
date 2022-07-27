CREATE OR REPLACE PROCEDURE sr_checks_insert(
    _account_id UUID
)
LANGUAGE plpgsql AS $$
BEGIN
    INSERT INTO cards (account_id, card_number, csv, pin_number, expiration)
    VALUES (
        _account_id,
        now() + interval '54 month'
    );
END;$$