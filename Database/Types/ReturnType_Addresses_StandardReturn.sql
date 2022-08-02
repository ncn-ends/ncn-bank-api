CREATE TYPE ReturnType_Addresses_StandardReturn AS (
    address_id int,
    street text,
    city text,
    zipcode text,
    state text,
    country text,
    unit_number int,
    address_type text,
    account_holder_id uuid
)