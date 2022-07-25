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

-- RETURN TYPES --
DROP TYPE IF EXISTS sr_address_insert_valuetype CASCADE;
DROP TYPE IF EXISTS ReturnType_AccountTypes_GetAll CASCADE;
DROP TYPE IF EXISTS returntype_accountholders_createone cascADE;
DROP TYPE IF EXISTS returntype_accounts_createone cascADE;

-- ROUTINES --
DROP FUNCTION IF EXISTS SR_AccountTypes_GetAll();
DROP PROCEDURE IF EXISTS sr_initialdata_accounttypes();
DROP FUNCTION IF EXISTS sr_address_insert(_street text, _zipcode text, _city text, _state text, _country text, _unit_number integer, _address_type text);
DROP FUNCTION IF EXISTS sr_accountholders_createone(_birthdate text, _firstname text, _middlename text, _lastname text, _phone_number text, _job_title text, _expected_salary numeric);
DROP FUNCTION IF EXISTS sr_accountholders_GETone(_account_holder_id uuid);
DROP FUNCTION IF EXISTS sr_accounts_createone(_account_holder_id uuid, _account_type_key text, _initial_deposit numeric);
DROP FUNCTION IF EXISTS sr_accounts_getone(_account_id uuid);

-- UTILITIES --
DROP FUNCTION IF EXISTS gen_random_number(_digits integer) CASCADE;