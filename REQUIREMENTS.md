## Accounts

### Types
- student checking account
  - comes with a debit card
  - no monthly fee
  - no overcharge fee
  - account must be linked to a standard or senior checking account
  - accountholder must be a student or under 18
- standard checking account
  - comes with a debit card
  - comes with paper checks
  - transfer to other bank accounts
  - minimum balance requirement to avoid monthly fee
  - $35 overcharge fee
  - rewards system
- savings account
  - interest revenue
    - APY (annual percentage yield)
  - minimum balance requirement
  - monthly maintenance fees?
  - 6 withdrawals per month
    - excess withdrawal fee
- High yield savings account
  - same as above, but requires higher monthly maintenance fees, higher minimum account balance, and less withdrawals per month
- CD Account
  - cd = certificate of deposit
  - deposit money to be locked away for a set period
    - this is called a maturity term
    - higher maturity term results in more APY
  - gain more interest on this money
  - when CD ends, get money or roll into a new CD
  - early withdrawal penalty

## Initial Data
### account_types
- student checking account
  - debit_card_access: true
  - paper_check_access: false
  - monthly_fee_id
    - monthly_fee_title: 'Monthly Maintenance Fee'
    - monthly_fee_description: null
    - monthly_fee_amount: 0.00
    - waive_fee_on_balance: null
  - overdraft_fee_amount: 0.00
  - holder_minimum_age: null
  - transfer_limit_id  
    - has_transfer_limit: false
    - transfer_limit_single: null
    - transfer_limit_cycle_flat: null
    - transfer_limit_cycle_quant: null
  - minimum_balance: 0.00
  - minimum_initial_deposit: 100.00
  - withdrawal_penalty_flat: null
  - withdrawal_penalty_rate: null
  - apy: 0.00
  - maturity_term_days: null

- standard checking account
  - debit_card_access: true
  - paper_check_access: true
  - monthly_fee_id
    - monthly_fee_title: 'Monthly Maintenance Fee'
    - monthly_fee_description: null
    - monthly_fee_amount: 15.00
    - waive_fee_on_balance: 300.00
  - overdraft_fee_amount: 35.00
  - holder_minimum_age: 18
  - transfer_limit_id  
    - has_transfer_limit: false
    - transfer_limit_single: null
    - transfer_limit_cycle_flat: null
    - transfer_limit_cycle_quant: null
  - minimum_balance: 0.00
  - minimum_initial_deposit: 300.00
  - withdrawal_penalty_flat: null
  - withdrawal_penalty_rate: null
  - apy: 0.00
  - maturity_term_days: null

- standard savings account
  - debit_card_access: false
  - paper_check_access: false
  - monthly_fee_id
    - monthly_fee_title: 'Monthly Maintenance Fee'
    - monthly_fee_description: null
    - monthly_fee_amount: 15.00
    - waive_fee_on_balance: 1000.00
  - overdraft_fee_amount: null
  - holder_minimum_age: 18
  - transfer_limit_id  
    - has_transfer_limit: true
    - transfer_limit_single: null
    - transfer_limit_cycle_flat: null
    - transfer_limit_cycle_quant: 6
  - minimum_balance: 300.00
  - minimum_initial_deposit: 300.00
  - withdrawal_penalty_flat: null
  - withdrawal_penalty_rate: null
  - apy: 0.01
  - maturity_term_days: null

- high-yield savings account
  - debit_card_access: false
  - paper_check_access: false
  - monthly_fee_id
    - monthly_fee_title: 'Monthly Maintenance Fee'
    - monthly_fee_description: null
    - monthly_fee_amount: 15.00
    - waive_fee_on_balance: 1000.00
  - overdraft_fee_amount: null
  - holder_minimum_age: 18
  - transfer_limit_id  
    - has_transfer_limit: true
    - transfer_limit_single: null
    - transfer_limit_cycle_flat: null
    - transfer_limit_cycle_quant: 2
  - minimum_balance: 10000.00
  - minimum_initial_deposit: 10000.00
  - withdrawal_penalty_flat: null
  - withdrawal_penalty_rate: null
  - apy: 0.25
  - maturity_term_days: null

- type 1 CD
  - debit_card_access: false
  - paper_check_access: false
  - monthly_fee_id
    - monthly_fee_title: 'Monthly Maintenance Fee'
    - monthly_fee_description: null
    - monthly_fee_amount: 15.00
    - waive_fee_on_balance: 1000.00
  - overdraft_fee_amount: null
  - holder_minimum_age: 18
  - transfer_limit_id  
    - has_transfer_limit: true
    - transfer_limit_single: null
    - transfer_limit_cycle_flat: null
    - transfer_limit_cycle_quant: 0
  - minimum_balance: 1000.00
  - minimum_initial_deposit: 1000.00
  - withdrawal_penalty_flat: $100
  - withdrawal_penalty_rate: .05
  - apy: .50
  - maturity_term_days: 28

- type 2 CD
  - debit_card_access: false
  - paper_check_access: false
  - monthly_fee_id
    - monthly_fee_title: 'Monthly Maintenance Fee'
    - monthly_fee_description: null
    - monthly_fee_amount: 15.00
    - waive_fee_on_balance: 1000.00
  - overdraft_fee_amount: null
  - holder_minimum_age: 18
  - transfer_limit_id  
    - has_transfer_limit: true
    - transfer_limit_single: null
    - transfer_limit_cycle_flat: null
    - transfer_limit_cycle_quant: 0
  - minimum_balance: 1000.00
  - minimum_initial_deposit: 1000.00
  - withdrawal_penalty_flat: $250
  - withdrawal_penalty_rate: .05
  - apy: .7
  - maturity_term_days: 56

- type 3 CD
  - debit_card_access: false
  - paper_check_access: false
  - monthly_fee_id
    - monthly_fee_title: 'Monthly Maintenance Fee'
    - monthly_fee_description: null
    - monthly_fee_amount: 15.00
    - waive_fee_on_balance: 1000.00
  - overdraft_fee_amount: null
  - holder_minimum_age: 18
  - transfer_limit_id  
    - has_transfer_limit: true
    - transfer_limit_single: null
    - transfer_limit_cycle_flat: null
    - transfer_limit_cycle_quant: 0
  - minimum_balance: 1000.00
  - minimum_initial_deposit: 1000.00
  - withdrawal_penalty_flat: $500
  - withdrawal_penalty_rate: .05
  - apy: 1.2
  - maturity_term_days: 112

## Schema 
- account_holder
  - account_holder_id
  - birthdate
  - firstname
  - middlename
  - lastname
  - phone_number
- account_types
  - account_type_id
  - name_internal
  - name_display
  - debit_card_access
  - paper_check_access
  - monthly_fee_id
  - overcharge_fee_amount
  - holder_minimum_age
  - transfer_limit_id
  - minimum_balance
  - minimum_initial_deposit
  - withdrawal_penalty_flat
  - withdrawal_penalty_rate
  - apy
  - maturity_term_days
- account
  - account_id
  - account_type_id
  - account_holder_id
- account_links
  - account_link_id
  - account_id_a
  - account_id_b
- monthly_fees
  - monthly_fee_id
  - monthly_fee_title
  - monthly_fee_description
  - monthly_fee_amount
  - waive_fee_on_balance
- transfer_limits
  - transfer_limit_id
  - has_transfer_limit
  - transfer_limit_single
  - transfer_limit_cycle_flat
  - transfer_limit_cycle_quant
- transfers
  - transfer_id
  - memo
  - amount
  - transfer_type
  - transfer_target
  - transfer_source?
  - card_id?
  - check_id?
- check
  - check_id
  - account_id
  - (research details on checks and add later)
- cards
  - card_id
  - account_id
  - pin_number'
- addresses
  - address_id
  - account_id
  - street
  - zipcode
  - city
  - state
  - country
  - unit_number?
  - address_type

## Types
- transfer_type
  - 'Check', 'Debit', 'Wire' 
- address_type
  - 'Apartment', 'House', 'Business'

## Triggers
- upon creating student checking account
  - if account_holder is over 18 and the TLD of the email is NOT .edu, then reject the creation of the account
  - if the account is not linked to another account that is a non-student-checking account, reject the creation of the account

## Actions
  - ~~Pull up info about an account holder~~
  - ~~Create a new account holder~~
    ~~- **make sure that it includes address creation**~~
  ~~- Get account holders addresses~~
  ~~- Get account holders cards~~
  ~~- Get account holders checks~~
  ~~- Get account holders transactions~~
  - ~~Get account holders balance~~
  - Deactivate account holder
    - Ensure that all accounts and cards are deactivated as well
    
  - ~~Create account attached to account holder~~
    ~~- Make initial deposit~~
  - ~~Get basic account info~~
  - ~~Search for account by account holder name~~
  - ~~Get account cards~~
  - ~~Get account checks~~
  - ~~Get account transactions~~
    - ~~both by target and by source~~
  - ~~Get account balance~~
  - ~~Deactivate account~~

  - ~~Create check attached to an account~~
  - ~~Deactivate check~~

  - ~~Create card attached to an account~~
  - ~~Deactivate card~~

  - ~~Create cash transfer~~
  - ~~Create card transfer~~
  - ~~Create check transfer~~
  
  ~~- Add additional address to holder~~
  ~~- Archive address~~