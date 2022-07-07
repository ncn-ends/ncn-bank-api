
-- TABLES --
DROP TABLE IF EXISTS account_types CASCADE;
DROP TABLE IF EXISTS account_holders CASCADE;
DROP TABLE IF EXISTS accounts CASCADE;
DROP TABLE IF EXISTS addresses CASCADE;

-- TYPES --
DROP TYPE IF EXISTS address_type CASCADE;

-- ROUTINES --
DROP PROCEDURE IF EXISTS sp_accountholders_insert(sp_birthdate text, sp_firstname text, sp_middlename text, sp_lastname text, sp_phone_number varchar, sp_street text, sp_zipcode varchar, sp_city text, sp_state text, sp_country text, sp_unit_number text, sp_address_type address_type);
DROP FUNCTION IF EXISTS udf_accountholders_getall();