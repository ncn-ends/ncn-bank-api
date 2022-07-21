CREATE OR REPLACE PROCEDURE SR_CreateTable_account_holders() AS $$
BEGIN
    CREATE TABLE account_holders (
          account_holder_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
          birthdate timestamptz NOT NULL,
          firstname text not null,
          middlename text,
          lastname text not null,
          phone_number VARCHAR(15) not null,
          job_title VARCHAR(64) NOT NULL,
          expected_salary MONEY NOT NULL
    );
END;
$$ LANGUAGE plpgsql;

CALL SR_CreateTable_account_holders();