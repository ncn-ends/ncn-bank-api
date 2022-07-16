CREATE OR REPLACE FUNCTION SR_AccountTypes_GetAll()
RETURNS setof account_types AS $$
BEGIN
    RETURN QUERY
    SELECT * FROM account_types;
END; $$ LANGUAGE plpgsql;