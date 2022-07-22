CREATE TABLE IF NOT EXISTS transfer_limits (
    transfer_limit_id SERIAL NOT NULL PRIMARY KEY,
    single_flat MONEY,
    cycle_flat MONEY,
    cycle_count INT
);