CREATE TABLE IF NOT EXISTS checks (
    check_id UUID PRIMARY KEY NOT NULL DEFAULT gen_random_uuid(),
    account_id UUID NOT NULL REFERENCES accounts(account_id),
    routing_number NUMERIC(9, 0) NOT NULL DEFAULT gen_random_number(9)::numeric(9, 0),
    expiration TIMESTAMPTZ NOT NULL DEFAULT now() + INTERVAL '180 day',
    deactivated BOOL DEFAULT false,
    created_at TIMESTAMPTZ DEFAULT now()
)