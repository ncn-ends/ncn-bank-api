CREATE VIEW View_ActiveAccounts AS
WITH account_wrap AS (
    SELECT
        (
            SELECT COUNT(*)
            FROM accounts a2
            WHERE a2.deactivated = a.account_id
        ) AS deactivated_count,
        *
    FROM accounts a
    WHERE a.deactivated IS NULL
)
SELECT *
FROM account_wrap
WHERE account_wrap.deactivated_count = 0;