CREATE OR REPLACE PROCEDURE SR_Checks_Insert(
    _account_id UUID,
    _pin_number numeric(4,0)
) AS
$$
    BEGIN
        INSERT INTO checks (
            account_id,
            pin_number
        ) VALUES (
            _account_id,
            _pin_number
        );
    END;
$$ LANGUAGE plpgsql;

CALL SR_Checks_Insert()