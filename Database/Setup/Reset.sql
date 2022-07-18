-- TABLES --
DROP TABLE IF EXISTS transfer_limits CASCADE;
DROP TABLE IF EXISTS monthly_fees CASCADE;
DROP TABLE IF EXISTS account_types CASCADE;
DROP TABLE IF EXISTS account_holders CASCADE;
DROP TABLE IF EXISTS accounts CASCADE;
DROP TABLE IF EXISTS addresses CASCADE;
DROP TABLE IF EXISTS cards CASCADE;
DROP TABLE IF EXISTS checks CASCADE;
DROP TABLE IF EXISTS transfers CASCADE;
DROP TABLE IF EXISTS branches CASCADE;
DROP TABLE IF EXISTS account_links CASCADE;

-- TYPES --
DROP TYPE IF EXISTS address_type CASCADE;
DROP TYPE IF EXISTS transfer_type CASCADE;

-- ROUTINES --
DROP PROCEDURE IF EXISTS sr_accountholders_insert(sp_birthdate text, sp_firstname text, sp_middlename text, sp_lastname text, sp_phone_number varchar, sp_street text, sp_zipcode varchar, sp_city text, sp_state text, sp_country text, sp_unit_number text, sp_address_type address_type);
DROP PROCEDURE IF EXISTS sr_accounts_insert(sp_account_holder_id uuid, sp_account_type_id integer);
drop PROCEDURE if exists sr_cards_insert(_account_id uuid, _pin_number numeric);
DROP FUNCTION IF EXISTS sr_accountholders_getall();
DROP FUNCTION IF EXISTS sr_accounts_getall();
DROP FUNCTION IF EXISTS sr_address_insert(_street text, _zipcode text, _city text, _state text, _country text, _unit_number integer, _address_type text);

-- UTILITIES --
DROP FUNCTION IF EXISTS gen_random_number(_digits integer) CASCADE;