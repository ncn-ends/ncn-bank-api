CREATE OR REPLACE FUNCTION ASDQWEJASD(pp int)
RETURNS SETOF int AS
$$
DECLARE
    has_row bool;
BEGIN
    has_row := (SELECT exists(SELECT 1 from account_types) as has_row);
    IF has_row THEN RETURN QUERY SELECT 1;
    ELSE RETURN QUERY SELECT -1;
    END IF;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM ASDQWEJASD(5);