CREATE OR REPLACE FUNCTION gen_check_number()
RETURNS numeric(9,0) AS $$
DECLARE
    min numeric(9,0);
    max numeric(9,0);
BEGIN
    min := 10 * 9;
    max := ('9' * 9)::int;
    RETURN floor(random() * (max - min + 1) + min);
END;$$ language plpgsql STRICT;

SELECT gen_check_number();