
DROP TABLE IF EXISTS accounts;
DROP PROCEDURE IF EXISTS CreateOneAccount(_email email);
DROP PROCEDURE IF EXISTS GetAllAccounts(_email email);
DROP DOMAIN IF EXISTS email CASCADE;

-- CREATING TYPES --
CREATE DOMAIN email AS text
  CHECK ( value ~ '^[a-zA-Z0-9.!#$%&''*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$' );

-- CREATING TABLES --

CREATE TABLE accounts (
    account_id UUID PRIMARY KEY NOT NULL DEFAULT gen_random_uuid(),
    email email NOT NULL
);

-- CREATING PROCEDURES --
CREATE OR REPLACE PROCEDURE CreateOneAccount(
    _email email
)
language plpgsql
as $$
begin
    INSERT INTO accounts (email)
    VALUES (_email);
end;$$;

CREATE OR REPLACE FUNCTION GetAllAccounts()
    RETURNS TABLE (
        account_id UUID,
        email email
    )
    language plpgsql AS
$$
begin
    RETURN QUERY
    SELECT
        accounts.account_id, accounts.email
    FROM
        accounts;
end;$$;
