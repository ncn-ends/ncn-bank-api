CREATE OR REPLACE FUNCTION gen_random_number(_digits int)
RETURNS numeric AS
$$
DECLARE
    min numeric;
    max numeric;
BEGIN
    min := 10 * _digits;
    max := REPEAT('9', _digits)::int;
    RETURN floor(random() * (max - min + 1) + min);
END; $$ LANGUAGE plpgsql STRICT;