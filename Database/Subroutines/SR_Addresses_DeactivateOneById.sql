CREATE OR REPLACE FUNCTION SR_Addresses_DeactivateOneById(
    _address_id uuid
)
RETURNS TABLE (
    address_id uuid
) AS $$
BEGIN
    RETURN QUERY
    WITH address_to_delete AS (
        SELECT
            addresses.address_id,
            addresses.street,
            addresses.zipcode,
            addresses.city,
            addresses.state,
            addresses.country,
            addresses.unit_number,
            addresses.address_type,
            addresses.account_holder_id
        FROM addresses
        WHERE addresses.address_id = _address_id
    )
    INSERT INTO addresses (
        street,
        zipcode,
        city,
        state,
        country,
        unit_number,
        address_type,
        account_holder_id,
        deactivated_address
    )
    VALUES (
        (SELECT address_to_delete.street from address_to_delete),
        (SELECT address_to_delete.zipcode from address_to_delete),
        (SELECT address_to_delete.city from address_to_delete),
        (SELECT address_to_delete.state from address_to_delete),
        (SELECT address_to_delete.country from address_to_delete),
        (SELECT address_to_delete.unit_number from address_to_delete),
        (SELECT address_to_delete.address_type from address_to_delete),
        (SELECT address_to_delete.account_holder_id from address_to_delete),
        (SELECT address_to_delete.address_id FROM address_to_delete)
    )
    RETURNING addresses.address_id AS address_id;
END ;$$ LANGUAGE plpgsql;