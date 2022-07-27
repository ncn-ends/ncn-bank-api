CREATE OR REPLACE FUNCTION SR_Accounts_GetRandomOne()
RETURNS SETOF accounts AS $$
BEGIN
    RETURN QUERY
    SELECT *
    FROM accounts
    ORDER BY random()
    LIMIT 1;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM SR_Accounts_GetRandomOne();