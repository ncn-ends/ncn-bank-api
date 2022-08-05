CREATE VIEW View_ActiveAddresses AS
WITH address_wrap AS (
    SELECT
        (
            SELECT COUNT(*)
            FROM addresses a2
            WHERE a2.deactivated_address = a.address_id
        ) AS deactivated_count,
        *
    FROM addresses a
    WHERE a.deactivated_address IS NULL
)
SELECT *
FROM address_wrap
WHERE address_wrap.deactivated_count = 0;