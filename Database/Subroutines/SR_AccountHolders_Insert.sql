-- insert into account_holder
-- then use the primary key of account_holder as the foreign key for address insertion

CREATE OR REPLACE PROCEDURE SR_AccountHolders_Insert(
    sp_birthdate TEXT,
    sp_firstname TEXT,
    sp_middlename TEXT,
    sp_lastname TEXT,
    sp_phone_number VARCHAR(15),
    sp_street TEXT,
    sp_zipcode VARCHAR(10),
    sp_city TEXT,
    sp_state TEXT,
    sp_country TEXT,
    sp_unit_number TEXT,
    sp_address_type address_type
)
language plpgsql
AS $$
    BEGIN
        WITH account_holder_insertion AS (
            INSERT INTO account_holders (
                  birthdate,
                  firstname,
                  middlename,
                  lastname,
                  phone_number
            ) VALUES (
                  to_timestamp(sp_birthdate, 'YYYY-MM-DD'),
                  sp_firstname,
                  sp_middlename,
                  sp_lastname,
                  sp_phone_number
            )
            RETURNING account_holder_id, firstname, lastname
        )
        INSERT INTO addresses (
            fk_account_holder_id,
            street,
            zipcode,
            city,
            state,
            country,
            unit_number,
            address_type
        )
        SELECT
              account_holder_insertion.account_holder_id,
              sp_street,
              sp_zipcode,
              sp_city,
              sp_state,
              sp_country,
              sp_unit_number,
              sp_address_type::address_type
        FROM account_holder_insertion;
    END;$$
