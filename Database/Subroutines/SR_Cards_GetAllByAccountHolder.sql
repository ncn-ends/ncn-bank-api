CREATE OR REPLACE FUNCTION SR_Cards_GetAllByAccountHolder(_account_holder_id uuid)
RETURNS setof ReturnType_Cards_StandardReturn AS $$
BEGIN
    RETURN QUERY
    SELECT
        view_activecards.card_id,
        view_activecards.card_number::TEXT,
        view_activecards.pin_number::TEXT,
        view_activecards.csv::TEXT,
        view_activecards.expiration,
        view_activecards.deactivated,
        view_activecards.created_at
    FROM view_activecards
    JOIN accounts a ON view_activecards.account_id = a.account_id
    JOIN account_holders ah ON a.account_holder_id = ah.account_holder_id
    WHERE ah.account_holder_id = _account_holder_id;
END;
$$ LANGUAGE plpgsql;