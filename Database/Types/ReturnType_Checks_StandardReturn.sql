CREATE TYPE ReturnType_Checks_StandardReturn AS (
    check_id uuid,
    account_number TEXT,
    routing_number TEXT,
    expiration TIMESTAMPTZ,
    deactivated BOOL,
    created_at timestamptz
);