
DROP TABLE IF EXISTS account_types CASCADE;
DROP TABLE IF EXISTS accounts CASCADE;
DROP TABLE IF EXISTS holders CASCADE;
DROP TABLE IF EXISTS transfers CASCADE;
DROP PROCEDURE IF EXISTS CreateOneAccount(_email email);
DROP PROCEDURE IF EXISTS GetAllAccounts(_email email);
DROP DOMAIN IF EXISTS email CASCADE;

-- CREATING TYPES --
CREATE DOMAIN email AS text
  CHECK ( value ~ '^[a-zA-Z0-9.!#$%&''*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$' );

-- CREATING TABLES --
-- CREATE TABLE account_types (
--     account_type_id SERIAL NOT NULL PRIMARY KEY,
--     name_code TEXT NOT NULL, -- the name of the account to be used internally
--     name_display TEXT NOT NULL, -- the name of the account that will be displayed for end users
--     apy decimal NOT NULL -- annual percentage yield (interest rate annually)
-- );
-- CREATE TABLE accounts (
--     account_id SERIAL NOT NULL PRIMARY KEY,
--     account_type_id SERIAL NOT NULL,
--     FOREIGN KEY(account_type_id) REFERENCES account_types(account_type_id)
-- );
-- CREATE TABLE holders (
--     holder_id UUID PRIMARY KEY NOT NULL DEFAULT gen_random_uuid(),
--     email email NOT NULL,
--     birthdate timestamptz not null
-- );
-- CREATE TABLE transfers (
--     transfer_id UUID PRIMARY KEY NOT NULL DEFAULT gen_random_uuid()
-- );

-- CREATING PROCEDURES --
-- CREATE OR REPLACE PROCEDURE CreateOneAccount(
--     _email email
-- )
-- language plpgsql
-- as
-- $$begin
--     INSERT INTO holders (email)
--     VALUES (_email);
-- end;$$;
--
-- CREATE OR REPLACE FUNCTION GetAllAccounts()
--     RETURNS TABLE (
--         account_id UUID,
--         email email
--     )
--     language plpgsql AS
-- $$begin
--     RETURN QUERY
--     SELECT
--         holders.holder_id, holders.email
--     FROM
--         holders;
-- end;$$;

-- INITIAL DATA --
-- INSERT INTO account_types (name_code, name_display)
-- VALUES
--     ('stu_ca', 'Student Checking Account'),