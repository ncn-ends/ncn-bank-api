using DataAccess.Models;

namespace DataAccess.Access;

public static class FakeInitialData
{
    public static readonly AccountHolderDTO SampleAccountHolder1 = new()
    {
        birthdate = new DateTime(),
        firstname = "Mike",
        middlename = "Gerard",
        lastname = "Tyson",
        phone_number = "111-111-1111",
        job_title = "Boxer",
        expected_salary = 1000000
    };
    
    public static readonly AccountHolderDTO SampleAccountHolder2 = new()
    {
        birthdate = new DateTime(),
        firstname = "James",
        middlename = "McGee",
        lastname = "Ajax",
        phone_number = "222-222-2222",
        job_title = "Investor",
        expected_salary = 10000000
    };

    public static CardCreationForm SampleCard1(Guid accountId) => new()
    {
        account_id = accountId,
        pin_number = "1234"
    };

    public static AccountFormDTO SampleAccount1(Guid holderId) => new()
    {
        account_holder_id = holderId,
        account_type_key = "stu_ca",
        initial_deposit = 1000000
    };
    
    public static AccountFormDTO SampleAccount2(Guid holderId) => new()
    {
        account_holder_id = holderId,
        account_type_key = "sta_ca",
        initial_deposit = 1000000
    };
}