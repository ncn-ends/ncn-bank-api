CREATE OR REPLACE FUNCTION SR_Addresses_GetAllByAccountHolder(
    _account_holder_id UUID
)
RETURNS SETOF Returntype_addresses_standardreturn AS $$
BEGIN
    RETURN QUERY
    SELECT
         address_id,
         street,
         city,
         zipcode::text,
         state,
         country,
         unit_number,
         address_type::text,
         account_holder_id
    FROM
        view_activeaddresses
    WHERE
        account_holder_id = _account_holder_id;
END;
$$ LANGUAGE plpgsql;