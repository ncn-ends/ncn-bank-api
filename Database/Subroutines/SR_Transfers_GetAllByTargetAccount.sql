CREATE OR REPLACE FUNCTION SR_Transfers_GetAllByTargetAccount(_account_id uuid)
RETURNS setof ReturnType_Transfers_StandardReturn AS $$
BEGIN
    RETURN QUERY
    WITH check_transfers AS (
        SELECT
            transfer_id,
            amount,
            memo::text,
            created_at
        FROM transfers_check
        WHERE transfer_target = _account_id
    ), card_transfers AS (
        SELECT
            transfer_id,
            amount,
            memo::text,
            created_at
        FROM transfers_card
        WHERE transfer_target = _account_id
    ), cash_transfers AS (
        SELECT
            transfer_id,
            amount,
            memo::text,
            created_at
        FROM transfers_cash
        WHERE transfer_target = _account_id
    )
    SELECT * FROM check_transfers
    UNION ALL
    SELECT * FROM card_transfers
    UNION ALL
    SELECT * FROM cash_transfers;
END; $$ LANGUAGE plpgsql;