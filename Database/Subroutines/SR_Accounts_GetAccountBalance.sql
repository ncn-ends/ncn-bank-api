CREATE OR REPLACE FUNCTION SR_Accounts_GetAccountBalance(_account_id uuid)
RETURNS TABLE (
    balance money
) AS $$
BEGIN
    RETURN QUERY
    WITH subtractions AS (
        SELECT
            CASE WHEN SUM(amount) IS NULL THEN 0::money ELSE SUM(amount) END
        FROM
            sr_transfers_getallbysourceaccount(_account_id)
    ), additions AS (
        SELECT
            CASE WHEN SUM(amount) IS NULL THEN 0::money ELSE SUM(amount) END
        FROM
            sr_transfers_getallbytargetaccount(_account_id)
    )
    SELECT
        ((SELECT additions.sum FROM additions) - (SELECT subtractions.sum FROM subtractions)) AS balance;
END; $$ LANGUAGE plpgsql;