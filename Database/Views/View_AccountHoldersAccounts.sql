CREATE VIEW View_AccountHoldersAccounts AS
SELECT
	accnt.account_id,
	accnt.account_type_id,
	accnt.routing_number,
	accnt.account_number,
    account_holders.account_holder_id,
    account_holders.birthdate,
	account_holders.firstname,
	account_holders.middlename,
	account_holders.lastname,
	account_holders.phone_number,
	account_holders.job_title,
	account_holders.expected_salary
FROM account_holders
JOIN accounts accnt ON account_holders.account_holder_id = accnt.account_holder_id;