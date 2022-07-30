-- CREATE TABLE IF NOT EXISTS transfers_cash (
--     transfer_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
--     amount MONEY NOT NULL CHECK (amount > 0.00::MONEY),
--     memo VARCHAR(250) NOT NULL DEFAULT '',

--     transfer_target UUID NOT NULL REFERENCES accounts(account_id),
--     branch_id INT NOT NULL REFERENCES branches(branch_id),
--     created_at TIMESTAMPTZ DEFAULT now()
-- );

CREATE TABLE IF NOT EXISTS transfers_check (
    transfer_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    amount MONEY NOT NULL CHECK (amount > 0.00::MONEY),
    memo VARCHAR(250) NOT NULL DEFAULT '',
    created_at TIMESTAMPTZ DEFAULT now(),

    transfer_target UUID NOT NULL REFERENCES accounts(account_id),
    check_id UUID NOT NULL REFERENCES checks(check_id)
);

CREATE TABLE IF NOT EXISTS transfers_card (
    transfer_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    amount MONEY NOT NULL CHECK (amount > 0.00::MONEY),
    memo VARCHAR(250) NOT NULL DEFAULT '',
    created_at TIMESTAMPTZ DEFAULT now(),

    transfer_target UUID NOT NULL REFERENCES accounts(account_id),
    card_id UUID NOT NULL REFERENCES cards(card_id)
);