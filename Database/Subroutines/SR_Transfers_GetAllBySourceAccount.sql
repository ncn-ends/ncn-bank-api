CREATE OR REPLACE FUNCTION SR_Transfers_GetAllBySourceAccount(_account_id uuid)
RETURNS setof ReturnType_Transfers_StandardReturn AS $$
BEGIN
    RETURN QUERY
    WITH check_transfers AS (
        SELECT
            transfers_check.transfer_id,
            transfers_check.amount,
            transfers_check.memo::text,
            transfers_check.created_at
        FROM transfers_check
        JOIN checks c ON transfers_check.check_id = c.check_id
        WHERE c.account_id = _account_id
    ), card_transfers AS (
        SELECT
            transfers_card.transfer_id,
            transfers_card.amount,
            transfers_card.memo::text,
            transfers_card.created_at
        FROM transfers_card
        JOIN cards c ON transfers_card.card_id = c.card_id
        WHERE c.account_id = _account_id
    )
    SELECT * FROM check_transfers
    UNION ALL
    SELECT * FROM card_transfers;
END; $$ LANGUAGE plpgsql;