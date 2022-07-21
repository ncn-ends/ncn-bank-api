CREATE OR REPLACE PROCEDURE SR_CreateTable_transfer_limits() AS
$$
BEGIN
    CREATE TABLE transfer_limits (
        transfer_limit_id SERIAL NOT NULL PRIMARY KEY,
        single_flat MONEY,
        cycle_flat MONEY,
        cycle_count INT
    );
END;
$$ LANGUAGE plpgsql