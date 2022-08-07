CREATE TYPE ReturnType_Cards_StandardReturn AS (
    card_id uuid,
    card_number TEXT,
    pin_number TEXT,
    csv TEXT,
    expiration TIMESTAMPTZ,
    deactivated UUID,
    created_at timestamptz
)