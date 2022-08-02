namespace DataAccess.Models;


// account_holder_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
// birthdate timestamptz NOT NULL,
// firstname text not null,
// middlename text,
// lastname text not null,
// phone_number VARCHAR(15) not null,
// job_title VARCHAR(64) NOT NULL,
// expected_salary MONEY NOT NULL
public class AccountHolderBO
{
    public Guid account_holder_id { get; set; }
    public DateTime birthdate { get; set; }
    public string firstname { get; set; } = "UNSET FIRST NAME";
    public string middlename { get; set; } = "UNSET MIDDLE NAME";
    public string lastname { get; set; } = "UNSET LAST NAME";
    public string phone_number { get; set; } = "UNSET PHONE NUMBER";
    public string job_title { get; set; } = "UNSET JOB TITLE";
    public decimal expected_salary { get; set; } = -1;
}

public class AccountHolderForm
{
    public DateTime birthdate { get; set; }
    public string? firstname { get; set; }
    public string? middlename { get; set; }
    public string? lastname { get; set; }
    public string? phone_number { get; set; }
    public string? job_title { get; set; }
    public decimal? expected_salary { get; set; }
    public string street { get; set; }
    public string zipcode { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string country { get; set; }
    public int unit_number { get; set; }
    public string address_type { get; set; }
}

public class AccountHolderInsertionResult
{
    public Guid account_holder_id { get; set; } = Guid.Empty;
}