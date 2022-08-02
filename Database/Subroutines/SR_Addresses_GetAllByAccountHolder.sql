CREATE OR REPLACE FUNCTION SR_Addresses_GetAllByAccountHolder(
    _account_holder_id UUID
)
RETURNS SETOF Returntype_addresses_standardreturn AS $$
BEGIN
    RETURN QUERY
    SELECT
        addresses.address_id,
        addresses.street,
        addresses.city,
        addresses.zipcode::text,
        addresses.state,
        addresses.country,
        addresses.unit_number::int,
        addresses.address_type::text,
        addresses.account_holder_id
    FROM addresses
    WHERE addresses.account_holder_id = _account_holder_id;
END;
$$ LANGUAGE plpgsql;