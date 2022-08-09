CREATE OR REPLACE FUNCTION SR_AccountHolders_DeactivateOneById(_account_holder_id uuid)
RETURNS setof account_holders AS $$
BEGIN
    PERFORM
        sr_accounts_deactivateonebyid(account.account_id)
    FROM
        view_activeaccounts account
    WHERE
        account.account_holder_id = _account_holder_id;

    PERFORM
        sr_addresses_deactivateonebyid(address.address_id)
    FROM
        view_activeaddresses address
    WHERE
        address.account_holder_id = _account_holder_id;

    RETURN QUERY
    WITH account_holder_to_deactivate AS (
        SELECT
            holder.birthdate,
            holder.firstname,
            holder.middlename,
            holder.lastname,
            holder.phone_number,
            holder.job_title,
            holder.expected_salary,
            holder.account_holder_id
        FROM view_activeaccountholders holder
        WHERE holder.account_holder_id = _account_holder_id
    )
    INSERT INTO account_holders (
        birthdate,
        firstname,
        middlename,
        lastname,
        phone_number,
        job_title,
        expected_salary,
        deactivated
    )
    VALUES (
        (SELECT birthdate FROM account_holder_to_deactivate),
        (SELECT firstname FROM account_holder_to_deactivate),
        (SELECT middlename FROM account_holder_to_deactivate),
        (SELECT lastname FROM account_holder_to_deactivate),
        (SELECT phone_number FROM account_holder_to_deactivate),
        (SELECT job_title FROM account_holder_to_deactivate),
        (SELECT expected_salary FROM account_holder_to_deactivate),
        (SELECT account_holder_id FROM account_holder_to_deactivate)
    )
    RETURNING *;
END;
$$ LANGUAGE plpgsql;