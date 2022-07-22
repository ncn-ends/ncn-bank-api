DO $$
BEGIN
    IF NOT EXISTS (Select 1 from pg_type WHERE typname = 'transfer_type') THEN
        CREATE TYPE transfer_type AS ENUM ('Bank', 'Debit', 'Cash');
    END IF;
END $$ LANGUAGE plpgsql;