CREATE TYPE ReturnType_Transfers_StandardReturn AS (
    transfer_id UUID,
    amount money,
    memo text,
    created_at timestamptz
)