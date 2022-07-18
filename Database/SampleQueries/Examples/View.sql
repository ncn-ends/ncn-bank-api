create or replace view TestTableA AS
select 1 as id, 'foo' as val
UNION ALL
select 2 as id, 'bar' as val;

create or replace view TestTableB AS
SELECT 1 as id, 'foo' as val
UNION ALL
SELECT 2 as id, 'bar' as val;
