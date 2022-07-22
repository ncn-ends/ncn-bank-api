CREATE TABLE IF NOT EXISTS transfers (
    transaction_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    amount MONEY NOT NULL CHECK (amount > 0.00::MONEY),
    memo VARCHAR(250) NOT NULL DEFAULT '',
    transfer_type transfer_type NOT NULL,
    transfer_target UUID NOT NULL REFERENCES accounts(account_id),
    transfer_source UUID REFERENCES accounts(account_id)  -- if cash, doesnt require a transfer source, so its nullable
)