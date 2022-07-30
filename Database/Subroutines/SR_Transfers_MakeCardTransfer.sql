CREATE TYPE ReturnType_Transfers_MakeCardTransfer AS (
    transfer_id UUID,
    amount MONEY,
    memo VARCHAR(250)
);

CREATE OR REPLACE FUNCTION SR_Transfers_MakeCardTransfer(_amount numeric, _card_number text, _transfer_target uuid, _memo text)
RETURNS setof ReturnType_Transfers_MakeCardTransfer AS $$
BEGIN
    RETURN QUERY
    WITH card_ref AS (
        SELECT cards.card_id
        FROM cards
        WHERE cards.card_number = _card_number::numeric(16, 0)
    )
    INSERT INTO transfers_card ( amount, transfer_target, card_id, memo )
    VALUES (
        _amount::money,
        _transfer_target,
        (SELECT card_id FROM card_ref),
        _memo
    )
    RETURNING
        transfers_card.transfer_id,
        transfers_card.amount,
        transfers_card.memo;
END; $$ LANGUAGE plpgsql;