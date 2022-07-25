CREATE OR REPLACE FUNCTION SR_AccountHolders_GetOne(
    _account_holder_id UUID
)
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
    WHERE account_holder_id = _account_holder_id;
END;
$$ LANGUAGE plpgsql;