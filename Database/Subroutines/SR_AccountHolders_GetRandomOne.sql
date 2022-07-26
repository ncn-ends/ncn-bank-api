CREATE OR REPLACE FUNCTION SR_AccountHolders_GetRandomOne()
RETURNS SETOF account_holders AS $$
BEGIN
    RETURN QUERY
    SELECT
        account_holder_id,
        birthdate,
        firstname,
        middlename,
        lastname,
        phone_number,
        job_title,
        expected_salary
    FROM account_holders
    ORDER BY random()
    LIMIT 1;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM account_holders;