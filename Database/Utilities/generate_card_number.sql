CREATE OR REPLACE FUNCTION generate_card_number()
RETURNS numeric(16,0) AS $$
DECLARE
    min numeric(16,0);
    max numeric(16,0);
BEGIN
    min := 100000000000000;
    max := 999999999999999;
    RETURN floor(random() * (max - min + 1) + min);
END;$$ language plpgsql STRICT;

