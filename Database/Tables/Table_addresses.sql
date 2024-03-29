CREATE TABLE IF NOT EXISTS addresses (
    address_id UUID PRIMARY KEY NOT NULL DEFAULT gen_random_uuid(),
    account_holder_id UUID REFERENCES account_holders(account_holder_id),
    street TEXT NOT NULL,
    zipcode VARCHAR(10) NOT NULL,
    city TEXT NOT NULL,
    state TEXT NOT NULL,
    country TEXT NOT NULL,
    unit_number TEXT DEFAULT NULL,
    address_type address_type NOT NULL,
    deactivated_address UUID REFERENCES addresses(address_id)
)