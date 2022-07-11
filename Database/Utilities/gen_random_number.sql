CREATE OR REPLACE FUNCTION gen_random_number(_digits int)
RETURNS numeric AS $$
DECLARE
    min numeric;
    max numeric;
    rng numeric;
BEGIN
    min := 10 * _digits;
    max := repeat('9', _digits)::int;
    rng :=  floor(random() * (max - min + 1) + min);
    return rng;
END; $$ LANGUAGE plpgsql STRICT;