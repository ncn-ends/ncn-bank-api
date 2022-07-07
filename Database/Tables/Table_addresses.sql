CREATE TABLE addresses (
    address_id SERIAL PRIMARY KEY NOT NULL,
    fk_account_holder_id UUID NOT NULL REFERENCES account_holders(account_holder_id),
    street TEXT NOT NULL,
    zipcode VARCHAR(10) NOT NULL,
    city TEXT NOT NULL,
    state TEXT NOT NULL,
    country TEXT NOT NULL,
    unit_number TEXT DEFAULT NULL,
    address_type address_type NOT NULL
)