CREATE OR REPLACE FUNCTION SR_Transfers_GetAllByAccountHolder(_account_holder_id uuid)
RETURNS setof ReturnType_Transfers_StandardReturn AS $$
BEGIN
    RETURN QUERY
    WITH card_transfers_from_account AS (
        SELECT
            transfers_card.transfer_id,
            transfers_card.amount * -1 AS amount,
            transfers_card.memo::text,
            transfers_card.created_at
        FROM transfers_card
        JOIN cards c ON transfers_card.card_id = c.card_id
        JOIN view_cardswithfullaccountdetails vc ON c.account_id = vc.account_id
        WHERE vc.account_holder_id = _account_holder_id
    ), card_transfers_to_account AS (
        SELECT
            transfers_card.transfer_id,
            transfers_card.amount AS amount,
            transfers_card.memo::text,
            transfers_card.created_at
        FROM transfers_card
        JOIN accounts a ON transfers_card.transfer_target = a.account_id
        JOIN account_holders ah ON a.account_holder_id = ah.account_holder_id
        WHERE ah.account_holder_id = _account_holder_id
    ), check_transfers_from_account AS (
        SELECT
            transfers_check.transfer_id,
            transfers_check.amount * -1 AS amount,
            transfers_check.memo::text,
            transfers_check.created_at
        FROM transfers_check
        JOIN checks c ON transfers_check.check_id = c.check_id
        JOIN view_cardswithfullaccountdetails vc ON c.account_id = vc.account_id
        WHERE vc.account_holder_id = _account_holder_id
    ), check_transfers_to_account AS (
        SELECT
            transfers_check.transfer_id,
            transfers_check.amount AS amount,
            transfers_check.memo::text,
            transfers_check.created_at
        FROM transfers_check
        JOIN accounts a ON transfers_check.transfer_target = a.account_id
        JOIN account_holders ah ON a.account_holder_id = ah.account_holder_id
        WHERE ah.account_holder_id = _account_holder_id
    ), cash_transfers_to_account AS (
        SELECT
            transfers_cash.transfer_id,
            transfers_cash.amount AS amount,
            transfers_cash.memo::text,
            transfers_cash.created_at
        FROM transfers_cash
        JOIN accounts a ON transfers_cash.transfer_target = a.account_id
        JOIN account_holders ah ON a.account_holder_id = ah.account_holder_id
        WHERE ah.account_holder_id = _account_holder_id
    )
    SELECT * FROM card_transfers_from_account
    UNION ALL
    SELECT * FROM card_transfers_to_account
    UNION ALL
    SELECT * FROM check_transfers_from_account
    UNION ALL
    SELECT * FROM check_transfers_to_account
    UNION ALL
    SELECT * FROM cash_transfers_to_account
    ORDER BY created_at DESC;
END; $$ LANGUAGE plpgsql;