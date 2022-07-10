CREATE TABLE cards (
    card_id UUID PRIMARY KEY NOT NULL DEFAULT gen_random_uuid(),
    account_id UUID NOT NULL REFERENCES accounts(account_id),
    card_number NUMERIC(16, 0) NOT NULL DEFAULT generate_card_number(),
    csv NUMERIC(3, 0) NOT NULL DEFAULT generate_csv_number(),
    pin_number NUMERIC(4, 0),
    expiration TIMESTAMPTZ NOT NULL
)