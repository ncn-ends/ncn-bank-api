CREATE TYPE ReturnType_Transfers_MakeCashTransfer AS (
    transfer_id UUID,
    amount MONEY,
    memo VARCHAR(250)
);

CREATE OR REPLACE FUNCTION SR_Transfers_MakeCashTransfer(_amount numeric, _transfer_target uuid, _memo text)
RETURNS setof ReturnType_Transfers_MakeCashTransfer AS $$
BEGIN
    RETURN QUERY
    INSERT INTO transfers_cash ( amount, transfer_target, memo )
    VALUES (
        _amount::money,
        _transfer_target,
        _memo
    )
    RETURNING
        transfers_cash.transfer_id,
        transfers_cash.amount,
        transfers_cash.memo;
END; $$ LANGUAGE plpgsql;