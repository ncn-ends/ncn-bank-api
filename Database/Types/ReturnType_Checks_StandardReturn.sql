CREATE TYPE ReturnType_Checks_StandardReturn AS (
    check_id uuid,
    account_number TEXT,
    routing_number TEXT,
    expiration TIMESTAMPTZ,
    deactivated UUID,
    created_at timestamptz
);