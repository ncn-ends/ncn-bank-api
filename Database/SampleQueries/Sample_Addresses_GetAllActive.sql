-- this query seems to work as intended, but there is a major problem
-- if a user inputs an address after deactivating it, it will remain deactive
-- also this query is just very confusing to read and understand
-- solution: change structure of table to make deactivated column reference the deactivated row
--      rather than a bool value

WITH goods AS (
    SELECT
        addresses.street,
        addresses.city,
        addresses.zipcode::text,
        addresses.state,
        addresses.country,
        addresses.unit_number::int,
        addresses.address_type::text,
        addresses.account_holder_id
    FROM
        addresses
    WHERE
        account_holder_id = '3f989b2b-c33f-44c2-accd-6c6f6b6dc093'
    GROUP BY
        addresses.street,
        addresses.city,
        addresses.zipcode::text,
        addresses.state,
        addresses.country,
        addresses.unit_number::int,
        addresses.address_type::text,
        addresses.account_holder_id
    HAVING
        sum(deactivated::int) = 0
), addresses_ordered AS (
    SELECT * FROM addresses
    ORDER BY deactivated::int ASC
)
SELECT
    a.address_id,
    goods.*
FROM goods
JOIN addresses a ON a.street = (SELECT street FROM goods);

-- SELECT * FROM addresses
-- JOIN account_holders ah ON addresses.account_holder_id = ah.account_holder_id
-- WHERE ah.account_holder_id = '3f989b2b-c33f-44c2-accd-6c6f6b6dc093';