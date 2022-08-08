CREATE VIEW View_ActiveChecks AS
WITH check_wrap AS (
    SELECT
        (
            SELECT COUNT(*)
            FROM checks c2
            WHERE c2.deactivated = c.check_id
        ) AS deactivated_count,
        *
    FROM checks c
    WHERE c.deactivated IS NULL
)
SELECT *
FROM check_wrap
WHERE check_wrap.deactivated_count = 0;