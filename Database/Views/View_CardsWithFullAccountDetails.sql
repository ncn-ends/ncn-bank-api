CREATE VIEW View_CardsWithFullAccountDetails AS
SELECT
    cards.card_id,
    cards.card_number,
    cards.csv,
    cards.pin_number,
    cards.expiration,
    cards.deactivated,
    cards.created_at,
    a.account_id,
    a.routing_number,
    a.account_number,
    a.account_type_id,
    ah.account_holder_id,
    ah.firstname,
    ah.middlename,
    ah.lastname,
    ah.birthdate,
    ah.phone_number,
    ah.expected_salary,
    ah.job_title
FROM cards
JOIN accounts a ON cards.account_id = a.account_id
JOIN account_holders ah ON a.account_holder_id = ah.account_holder_id