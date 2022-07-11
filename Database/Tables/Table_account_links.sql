CREATE TABLE account_links (
    account_link_id SERIAL PRIMARY KEY,
    node_parent UUID NOT NULL REFERENCES accounts(account_id),
    node_child UUID NOT NULL REFERENCES accounts(account_id)
)