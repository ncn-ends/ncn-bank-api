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
}