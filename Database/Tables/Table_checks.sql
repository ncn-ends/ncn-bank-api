CREATE TABLE IF NOT EXISTS checks (
    check_id UUID PRIMARY KEY NOT NULL DEFAULT gen_random_uuid(),
    account_id UUID NOT NULL REFERENCES accounts(account_id),
    routing_number NUMERIC(16, 0) NOT NULL DEFAULT gen_random_number(16)::numeric(16, 0),
    csv NUMERIC(3, 0) NOT NULL DEFAULT gen_random_number(3)::numeric(3, 0),
    pin_number NUMERIC(4, 0),
    expiration TIMESTAMPTZ NOT NULL DEFAULT now() + INTERVAL '54 month'
)