CREATE TABLE IF NOT EXISTS cards (
    card_id UUID PRIMARY KEY NOT NULL DEFAULT gen_random_uuid(),
    account_id UUID NOT NULL REFERENCES accounts(account_id),
    card_number NUMERIC(16, 0) NOT NULL DEFAULT gen_random_number(16),
    csv NUMERIC(3, 0) NOT NULL DEFAULT gen_random_number(3),
    pin_number NUMERIC(4, 0),
    expiration TIMESTAMPTZ NOT NULL DEFAULT now() + INTERVAL '54 month',
    deactivated UUID REFERENCES cards(card_id),
    created_at TIMESTAMPTZ DEFAULT now()
)