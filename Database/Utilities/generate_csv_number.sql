CREATE OR REPLACE FUNCTION generate_csv_number()
returns NUMERIC(3,0) AS $$
DECLARE
    min NUMERIC(3,0);
    max NUMERIC(3,0);
BEGIN
    min := 100;
    max := 999;
    RETURN floor(random() * (max - min + 1) + min);
END;$$ language plpgsql STRICT;

