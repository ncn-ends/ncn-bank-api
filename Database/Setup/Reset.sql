-- TABLES --
DROP TABLE IF EXISTS transfer_limits CASCADE;
DROP TABLE IF EXISTS monthly_fees CASCADE;
DROP TABLE IF EXISTS account_types CASCADE;
DROP TABLE IF EXISTS account_holders CASCADE;
DROP TABLE IF EXISTS accounts CASCADE;
DROP TABLE IF EXISTS addresses CASCADE;
DROP TABLE IF EXISTS cards CASCADE;
DROP TABLE IF EXISTS checks CASCADE;
DROP TABLE IF EXISTS transfers_check CASCADE;
DROP TABLE IF EXISTS transfers_cash CASCADE;
DROP TABLE IF EXISTS transfers_card CASCADE;
DROP TABLE IF EXISTS branches CASCADE;
DROP TABLE IF EXISTS account_links CASCADE;

-- VIEWS --
DROP VIEW IF EXISTS view_accountholdersaccounts cascade;

-- TYPES --
DROP TYPE IF EXISTS address_type CASCADE;
DROP TYPE IF EXISTS transfer_type CASCADE;

-- RETURN TYPES --
DROP TYPE IF EXISTS sr_address_insert_valuetype CASCADE;
DROP TYPE IF EXISTS ReturnType_AccountTypes_GetAll CASCADE;
DROP TYPE IF EXISTS returntype_accountholders_createone cascADE;
DROP TYPE IF EXISTS returntype_accounts_createone cascADE;
DROP TYPE IF EXISTS ReturnType_Cards_CreateOne CASCADE;
DROP TYPE IF EXISTS ReturnType_Checks_CreateOne CASCADE;
DROP TYPE IF EXISTS returntype_checks_deactivateonebyid CASCADE;
DROP TYPE IF EXISTS returntype_cards_deactivateonebyid CASCADE;
DROP TYPE IF EXISTS ReturnType_Checks_GetRandomOne CASCADE;
DROP TYPE IF EXISTS ReturnType_Transfers_MakeCheckTransfer CASCADE;
DROP TYPE IF EXISTS ReturnType_Transfers_MakeCardTransfer CASCADE;
DROP TYPE IF EXISTS ReturnType_Cards_GetRandomOne CASCADE;
DROP TYPE IF EXISTS returntype_transfers_makecashtransfer CASCADE;
DROP TYPE IF EXISTS ReturnType_Transfers_StandardReturn CASCADE;

-- ROUTINES --
DROP FUNCTION IF EXISTS SR_AccountTypes_GetAll();
DROP PROCEDURE IF EXISTS sr_initialdata_accounttypes();
DROP FUNCTION IF EXISTS sr_address_insert(_street text, _zipcode text, _city text, _state text, _country text, _unit_number integer, _address_type text);
DROP FUNCTION IF EXISTS sr_accountholders_createone(_birthdate text, _firstname text, _middlename text, _lastname text, _phone_number text, _job_title text, _expected_salary numeric);
DROP FUNCTION IF EXISTS sr_accountholders_GETone(_account_holder_id uuid);
DROP FUNCTION IF EXISTS sr_accountholders_GETRandomone();
DROP FUNCTION IF EXISTS sr_accounts_createone(_account_holder_id uuid, _account_type_key text, _initial_deposit numeric);
DROP FUNCTION IF EXISTS sr_accounts_getone(_account_id uuid);
DROP FUNCTION if exists sr_accounts_getrandomone();
DROP FUNCTION IF EXISTS sr_cards_insert(_account_id uuid, _pin_number numeric);
DROP FUNCTION IF EXISTS sr_checks_deactivateonebyid(_check_id uuid);
DROP FUNCTION IF EXISTS sr_checks_insert(_account_id uuid);
DROP FUNCTION IF EXISTS sr_checks_deactivateonebyid(_check_id uuid);
DROP FUNCTION IF EXISTS SR_Checks_GetRandomOne();
DROP FUNCTION IF EXISTS SR_Transfers_MakeCheckTransfer(_amount money, _routing_number text, _transfer_target uuid, _memo text);
DROP FUNCTION IF EXISTS sr_accounts_searchbyholdername(_name text);
DROP FUNCTION IF EXISTS sr_transfers_makechecktransfer(_amount numeric, _routing_number text, _transfer_target uuid, _memo text);
DROP FUNCTION IF EXISTS sr_cards_getrandomone();
DROP FUNCTION IF EXISTS sr_transfers_makecashtransfer(_amount numeric, _transfer_target uuid, _memo text);

-- UTILITIES --
DROP FUNCTION IF EXISTS gen_random_number(_digits integer) CASCADE;