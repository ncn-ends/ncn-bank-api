CREATE TYPE ReturnType_Transfers_MakeCheckTransfer AS (
    transfer_id UUID,
    amount MONEY,
    memo VARCHAR(250)
);

CREATE OR REPLACE FUNCTION SR_Transfers_MakeCheckTransfer(_amount numeric, _routing_number text, _transfer_target uuid, _memo text)
RETURNS setof ReturnType_Transfers_MakeCheckTransfer AS $$
BEGIN
    RETURN QUERY
    WITH check_ref AS (
        SELECT checks.check_id
        FROM checks
        WHERE checks.routing_number = _routing_number::numeric(9, 0)
    )
    INSERT INTO transfers_check ( amount, transfer_target, check_id, memo )
    VALUES (
        _amount::money,
        _transfer_target,
        (SELECT check_id FROM check_ref),
        _memo
    )
    RETURNING
        transfers_check.transfer_id,
        transfers_check.amount,
        transfers_check.memo;
END; $$ LANGUAGE plpgsql;

-- account_holder requests to create checks
    -- attached to account
-- account_holder writes a check
-- account_holder physically hands the check to somebody else
-- somebody else cashes the check
    --