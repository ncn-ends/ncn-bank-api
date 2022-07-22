CREATE OR REPLACE PROCEDURE SR_accounts_insert(
    sp_account_holder_id UUID,
    sp_account_type_id INT
)
language plpgsql AS
$$
BEGIN
    INSERT INTO accounts (account_holder_id, account_type_id)
    VALUES (sp_account_holder_id, sp_account_type_id);
END;$$