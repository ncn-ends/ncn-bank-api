
-- DROP TYPE person_dataset CASCADE;
-- CREATE TYPE person_dataset AS (
--     id UUID,
--     name TEXT,
--     age INT
-- );

-- CREATE TABLE persons OF person_dataset;

CREATE TABLE persons (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name TEXT,
    age INT
);

INSERT INTO persons (name, age)
VALUES
    ('peter', 18),
    ('james', 19);

CREATE OR REPLACE FUNCTION read_all_persons()
RETURNS SETOF persons AS $$
    BEGIN
        RETURN QUERY
        SELECT persons.id, persons.name, persons.age
        FROM persons;
    END;
    $$ LANGUAGE plpgsql;

SELECT * FROM read_all_persons();

DROP FUNCTION read_all_persons();
DROP TABLE persons CASCADE;

-- CREATE DOMAIN comp_foo AS raw_comp_foo
-- CHECK ((VALUE).min_value < (VALUE).max_value);