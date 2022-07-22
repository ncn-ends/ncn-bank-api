CREATE TABLE IF NOT EXISTS accounts (
    account_id UUID NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
    account_holder_id UUID NOT NULL REFERENCES account_holders(account_holder_id),
    account_type_id INT NOT NULL REFERENCES account_types(account_type_id),
    routing_number NUMERIC(9, 0) NOT NULL DEFAULT gen_random_number(9),
    account_number NUMERIC(9, 0) NOT NULL DEFAULT gen_random_number(9),
    balance MONEY NOT NULL DEFAULT 0.00
)