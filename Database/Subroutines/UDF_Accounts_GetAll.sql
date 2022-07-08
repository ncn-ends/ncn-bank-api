CREATE OR REPLACE FUNCTION UDF_Accounts_GetAll()
RETURNS TABLE (
        account_id UUID,
        account_holder_id UUID,
        birthdate timestamptz,
        firstname TEXT,
        middlename TEXT,
        lastname TEXT,
        street TEXT,
        zipcode VARCHAR(10),
        city TEXT,
        state TEXT,
        country TEXT,
        unit_number TEXT,
        address_type address_type
)
LANGUAGE plpgsql AS
$$BEGIN
    RETURN QUERY
    SELECT
        accounts.account_id,
        accounts.account_holder_id,
        ah.birthdate,
        ah.firstname,
        ah.middlename,
        ah.lastname,
        addr.street,
        addr.zipcode,
        addr.city,
        addr.state,
        addr.country,
        addr.unit_number,
        addr.address_type
    FROM accounts
    JOIN account_holders ah ON accounts.account_holder_id = ah.account_holder_id
    JOIN addresses addr ON ah.account_holder_id = addr.fk_account_holder_id;
END;$$