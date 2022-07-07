SELECT * FROM account_holders
JOIN addresses a ON account_holders.account_holder_id = a.fk_account_holder_id;