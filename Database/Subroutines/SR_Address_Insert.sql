CREATE TYPE SR_Address_Insert_ValueType AS (address_id int);

CREATE OR REPLACE FUNCTION SR_Address_Insert(
    _street TEXT,
    _zipcode TEXT,
    _city TEXT,
    _state TEXT,
    _country TEXT,
    _unit_number INT,
    _address_type TEXT,
    _account_holder_id UUID
)
RETURNS SETOF SR_Address_Insert_ValueType AS $$
BEGIN
    RETURN QUERY
    INSERT INTO addresses (street, zipcode, city, state, country, unit_number, address_type, account_holder_id)
    VALUES (
        _street,
        _zipcode,
        _city,
        _state,
        _country,
        _unit_number,
        _address_type::address_type,
        _account_holder_id
    )
    RETURNING addresses.address_id AS address_id;
END ;$$ LANGUAGE plpgsql;