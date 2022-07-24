CREATE TYPE ReturnType_AccountHolders_CreateOne AS (account_holder_id uuid);

CREATE OR REPLACE FUNCTION SR_AccountHolders_CreateOne(
    _birthdate timestamptz,
    _firstname text,
    _middlename text,
    _lastname text,
    _phone_number text,
    _job_title text,
    _expected_salary numeric
)
RETURNS SETOF ReturnType_AccountHolders_CreateOne AS $$
BEGIN
    RETURN QUERY
    INSERT INTO account_holders (birthdate, firstname, middlename, lastname, phone_number, job_title, expected_salary)
    VALUES (
        _birthdate,
        _firstname,
        _middlename,
        _lastname,
        _phone_number::VARCHAR(15),
        _job_title::VARCHAR(64),
        _expected_salary::money
    ) RETURNING account_holders.account_holder_id AS account_holder_id;
END;
$$ LANGUAGE plpgsql;