CREATE OR REPLACE FUNCTION SR_Accounts_SearchByHolderName(
    _name text
)
RETURNS SETOF View_AccountHoldersAccounts AS $$
BEGIN
    RETURN QUERY
    SELECT *
    FROM View_AccountHoldersAccounts
    WHERE
        lastname ILIKE ('%' || _name || '%') OR
        firstname ILIKE ('%' || _name || '%') OR
        middlename ILIKE ('%' || _name || '%');

END;
$$ LANGUAGE plpgsql;