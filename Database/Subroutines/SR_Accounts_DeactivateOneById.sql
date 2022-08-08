CREATE OR REPLACE FUNCTION SR_Accounts_DeactivateOneById(_account_id uuid)
RETURNS setof accounts AS $$
BEGIN
    PERFORM
        sr_cards_deactivateonebyid(cards.card_id)
    FROM
        view_activecards cards
    WHERE
        cards.account_id = _account_id;

    PERFORM
        sr_checks_deactivateonebyid(checks.check_id)
    FROM
        view_activechecks checks
    WHERE
        checks.account_id = _account_id;

    RETURN QUERY
    WITH account_to_deactivate AS (
        SELECT
            account_holder_id,
            account_type_id,
            routing_number,
            account_number,
            account_id
        FROM view_activeaccounts account
        WHERE account.account_id = _account_id
    )
    INSERT INTO accounts (
        account_holder_id,
        account_type_id,
        routing_number,
        account_number,
        deactivated
    )
    VALUES (
        (SELECT account_holder_id FROM account_to_deactivate),
        (SELECT account_type_id FROM account_to_deactivate),
        (SELECT routing_number FROM account_to_deactivate),
        (SELECT account_number FROM account_to_deactivate),
        (SELECT account_id FROM account_to_deactivate)
    )
    RETURNING *;
END;
$$ LANGUAGE plpgsql;