create or replace function UDF_AccountHolders_GetAll()
RETURNS TABLE (
    account_holder_id UUID,
    firstname TEXT,
    middlename TEXT,
    lastname TEXT,
    phone_number VARCHAR(15),
    birthdate TIMESTAMPTZ,
    street TEXT,
    zipcode VARCHAR(10),
    city TEXT,
    state TEXT,
    country TEXT,
    unit_number TEXT,
    address_type address_type
)
LANGUAGE plpgsql AS
$$
BEGIN
    RETURN QUERY
    SELECT
        account_holders.account_holder_id,
        account_holders.firstname,
        account_holders.middlename,
        account_holders.lastname,
        account_holders.phone_number,
        account_holders.birthdate,
        a.street,
        a.zipcode,
        a.city,
        a.state,
        a.country,
        a.unit_number,
        a.address_type
    FROM account_holders
    JOIN addresses a ON account_holders.account_holder_id = a.fk_account_holder_id;
END;$$
