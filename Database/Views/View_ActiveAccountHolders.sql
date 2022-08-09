CREATE VIEW View_ActiveAccountHolders AS
WITH holder_wrap AS (
    SELECT
        (
            SELECT COUNT(*)
            FROM account_holders ah2
            WHERE ah2.deactivated = ah.account_holder_id
        ) AS deactivated_count,
        *
    FROM account_holders ah
    WHERE ah.deactivated IS NULL
)
SELECT *
FROM holder_wrap
WHERE holder_wrap.deactivated_count = 0;