CREATE OR REPLACE FUNCTION something(pp int)
RETURNS setof int AS
$$
BEGIN
    IF pp = 5 THEN RETURN QUERY SELECT -1;
    ELSE RETURN QUERY SELECT 1;
    END IF;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM something(5);