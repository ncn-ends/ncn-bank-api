-- meant to illustrate how to perform the query without SP
-- good showcase for chaining CTEs

WITH input_data(
    birthdate,
    firstname,
    middlename,
    lastname,
    phone_number,
    street,
    zipcode,
    city,
    state,
    country,
    unit_number,
    address_type
    )
    AS (
        VALUES (
            TO_TIMESTAMP('2016-04-02', 'YYYY-MM-DD'),
            'peter',
            'p',
            'piper',
            '111-111-1111',
            '123 Hello Ave',
            '11111',
            'The City',
            'The State',
            'United States',
            '100',
            'Condo/Apartment'::address_type
        )
), account_holder_insertion AS (
    INSERT INTO account_holders (
          birthdate,
          firstname,
          middlename,
          lastname,
          phone_number
    ) SELECT
        input_data.birthdate,
        input_data.firstname,
        input_data.middlename,
        input_data.lastname,
        input_data.phone_number
    FROM input_data
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
      input_data.street,
      input_data.zipcode,
      input_data.city,
      input_data.state,
      input_data.country,
      input_data.unit_number,
      input_data.address_type
FROM input_data
JOIN account_holder_insertion USING (firstname, lastname)
RETURNING address_id, fk_account_holder_id AS account_holder_id;
