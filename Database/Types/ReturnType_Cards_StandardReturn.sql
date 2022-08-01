CREATE TYPE ReturnType_Cards_StandardReturn AS (
    card_id uuid,
    card_number TEXT,
    pin_number TEXT,
    expiration TIMESTAMPTZ,
    deactivated BOOL,
    created_at timestamptz
)