CREATE TYPE ReturnType_AccountHolders_CreateOne AS (account_holder_id uuid);

CREATE OR REPLACE FUNCTION SR_AccountHolders_CreateOne(
    _birthdate timestamptz,
    _firstname text,
    _middlename text,
    _lastname text,
    _phone_number text,
    _job_title text,
    _expected_salary numeric,
    _street TEXT,
    _zipcode TEXT,
    _city TEXT,
    _state TEXT,
    _country TEXT,
    _unit_number INT,
    _address_type TEXT
)
RETURNS SETOF ReturnType_AccountHolders_CreateOne AS $$
BEGIN
    RETURN QUERY
    WITH account_holder_insertion AS (
        INSERT INTO account_holders (birthdate, firstname, middlename, lastname, phone_number, job_title, expected_salary)
        VALUES (
            _birthdate,
            _firstname,
            _middlename,
            _lastname,
            _phone_number::VARCHAR(15),
            _job_title::VARCHAR(64),
            _expected_salary::money
        ) RETURNING account_holders.account_holder_id AS account_holder_id
    ), nothing AS (
        INSERT INTO addresses (
           street,
            zipcode,
            city,
            state,
            country,
            unit_number,
            address_type,
            account_holder_id
        )
        VALUES (
            _street,
            _zipcode,
            _city,
            _state,
            _country,
            _unit_number,
            _address_type::address_type,
            (SELECT account_holder_id FROM account_holder_insertion)
        )
    )
    SELECT * FROM account_holder_insertion;
END; $$ LANGUAGE plpgsql;