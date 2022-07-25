CREATE OR REPLACE FUNCTION SR_Accounts_GetOne(
    _account_id UUID
)
RETURNS SETOF accounts AS $$
BEGIN
    RETURN QUERY

    SELECT *
    FROM accounts
    WHERE accounts.account_id = _account_id;
END;
$$ LANGUAGE plpgsql;