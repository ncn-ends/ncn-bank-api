-- TABLES --
DROP TABLE IF EXISTS account_types CASCADE;
DROP TABLE IF EXISTS account_holders CASCADE;
DROP TABLE IF EXISTS accounts CASCADE;
DROP TABLE IF EXISTS addresses CASCADE;
DROP TABLE IF EXISTS cards CASCADE;

-- TYPES --
DROP TYPE IF EXISTS address_type CASCADE;

-- ROUTINES --
DROP PROCEDURE IF EXISTS sr_accountholders_insert(sp_birthdate text, sp_firstname text, sp_middlename text, sp_lastname text, sp_phone_number varchar, sp_street text, sp_zipcode varchar, sp_city text, sp_state text, sp_country text, sp_unit_number text, sp_address_type address_type);
DROP PROCEDURE IF EXISTS sr_accounts_insert(sp_account_holder_id uuid, sp_account_type_id integer);
drop PROCEDURE if exists sr_cards_insert(_account_id uuid, _pin_number numeric);
DROP FUNCTION IF EXISTS sr_accountholders_getall();
DROP FUNCTION IF EXISTS sr_accounts_getall();

-- UTILITIES --
DROP FUNCTION IF EXISTS generate_card_number();
DROP FUNCTION IF EXISTS generate_csv_number();