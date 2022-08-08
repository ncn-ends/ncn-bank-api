CREATE OR REPLACE FUNCTION SR_Accounts_GetOne(
    _account_id UUID
)
RETURNS SETOF accounts AS $$
BEGIN
    RETURN QUERY

    SELECT
    	account_id,
	    account_holder_id,
	    account_type_id,
	    routing_number,
	    account_number,
	    deactivated
    FROM view_activeaccounts accounts
    WHERE accounts.account_id = _account_id;
END;
$$ LANGUAGE plpgsql;