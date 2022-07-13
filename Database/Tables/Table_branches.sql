CREATE TABLE branches (
    branch_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    fk_address_id INT NOT NULL REFERENCES addresses(address_id),
    region_number NUMERIC(9, 0) DEFAULT gen_random_number(9)::NUMERIC(9, 0)
)