CREATE OR REPLACE FUNCTION gen_random_number_cast(_digits int)
RETURNS numeric AS $$
DECLARE
    min numeric;
    max numeric;
    rng numeric;
BEGIN
    min := power(10, _digits - 1);
    max := repeat('9', _digits)::numeric;
    rng :=  floor(random() * (max - min + 1) + min);
    return rng;
END; $$ LANGUAGE plpgsql STRICT;

CREATE OR REPLACE FUNCTION gen_random_number(_digits int)
RETURNS numeric AS $$
DECLARE
    min numeric;
    max numeric;
    rng numeric;
BEGIN
    min := POWER(10, _digits - 1);
    max := POWER(10, _digits) - 1;
    rng :=  floor(random() * (max - min + 1) + min);
    return rng;
END; $$ LANGUAGE plpgsql STRICT;

-- SELECT gen_random_number(9)
-- UNION
-- SELECT gen_random_number_cast(9)

-- EXPLAIN ANALYZE SELECT gen_random_number_cast(16) FROM generate_series(1, 100000);
-- EXPLAIN ANALYZE SELECT gen_random_number(16) FROM generate_series(1, 100000);