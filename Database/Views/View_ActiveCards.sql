CREATE VIEW View_ActiveCards AS
WITH card_wrap AS (
    SELECT
        (
            SELECT COUNT(*)
            FROM cards c2
            WHERE c2.deactivated = c.card_id
        ) AS deactivated_count,
        *
    FROM cards c
    WHERE c.deactivated IS NULL
)
SELECT *
FROM card_wrap
WHERE card_wrap.deactivated_count = 0;