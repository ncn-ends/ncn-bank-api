CREATE OR REPLACE FUNCTION sr_accountholders_getrandomone() RETURNS SETOF account_holders AS $$
BEGIN
    RETURN QUERY SELECT
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
END; $$ LANGUAGE plpgsql;