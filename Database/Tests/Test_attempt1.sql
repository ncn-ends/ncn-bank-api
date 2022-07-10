-- https://dba.stackexchange.com/questions/141419/postgresql-pass-table-as-argument-in-function/141766#141766

CREATE OR REPLACE FUNCTION assert(_tableA regclass, _tableB regclass) RETURNS void AS
$$
    DECLARE
        differences int;
    BEGIN
        SELECT COUNT(1)
        FROM _tableA a
        FULL OUTER JOIN _tableB b USING (id, val)
        WHERE
            a.id IS NULL
           OR b.id IS NULL
        INTO differences;

        IF differences != 0 THEN
            RAISE NOTICE 'Bad boy';
        END IF;
    END
$$ LANGUAGE plpgsql;


create or replace view TestTableA AS
select 1 as id, 'foo' as val
UNION ALL
select 2 as id, 'bar' as val;

create or replace view TestTableB AS
SELECT 1 as id, 'foo' as val
UNION ALL
SELECT 2 as id, 'bar' as val;

select * from assert('TestTableA', 'TestTableB');