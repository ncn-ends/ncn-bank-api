CREATE OR REPLACE FUNCTION SR_AccountHolders_GetOverallBalance(_holder_id uuid)
RETURNS TABLE (
    balance money
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        sum((SELECT * FROM SR_Accounts_getaccountbalance(a.account_id))) AS balance
    FROM
        account_holders
    JOIN
        accounts a ON account_holders.account_holder_id = a.account_holder_id
    WHERE
        account_holders.account_holder_id = _holder_id;
END; $$ LANGUAGE plpgsql;