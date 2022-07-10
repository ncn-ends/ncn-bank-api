DROP TABLE IF EXISTS persons CASCADE;
CREATE TABLE persons (
    id INT PRIMARY KEY,
    name TEXT
);

INSERT INTO persons
VALUES
    (1, 'peter'),
    (2, 'james');

CREATE OR REPLACE FUNCTION process(x_source persons[]) RETURNS setof persons AS
$$
    BEGIN
        return query
        SELECT * FROM unnest(x_source);
    end;
$$ LANGUAGE plpgsql;

select * from
  process(
    array(
      select
        row(id, name)::persons
      from persons
    )
);

DROP TABLE persons CASCADE;