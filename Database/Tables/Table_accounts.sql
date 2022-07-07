CREATE TABLE accounts (
    account_id UUID NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
    account_holder_id UUID NOT NULL REFERENCES account_holders(account_holder_id),
    account_type_id INT NOT NULL REFERENCES account_types(account_type_id)
)