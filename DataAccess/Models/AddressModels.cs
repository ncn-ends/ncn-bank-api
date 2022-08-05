namespace DataAccess.Models;

public class AddressBO
{
    public Guid address_id { get; set; }
    public string street { get; set; } = "UNSET STREET ADDRESS";
    public string zipcode { get; set; } = "UNSET ZIPCODE";
    public string city { get; set; } = "UNSET CITY";
    public string state { get; set; } = "UNSET STATE";
    public string country { get; set; } = "UNSET COUNTRY";
    public int unit_number { get; set; } = -1;
    public string address_type { get; set; } = "UNSET ADDRESS TYPE";
    public Guid? account_holder_id { get; set; }
    public Guid? deactivated_address { get; set; } = null;
}

public class AddressInsertionForm
{
    
    public string street { get; set; } = "UNSET STREET ADDRESS";
    public string zipcode { get; set; } = "UNSET ZIPCODE";
    public string city { get; set; } = "UNSET CITY";
    public string state { get; set; } = "UNSET STATE";
    public string country { get; set; } = "UNSET COUNTRY";
    public int unit_number { get; set; } = -1;
    public string address_type { get; set; } = "UNSET ADDRESS TYPE";
    public Guid? account_holder_id { get; set; }
    public Guid? deactivated_address { get; set; } = null;
}

public class AddressInsertionReturnType
{
    public Guid address_id { get; set; } = Guid.Empty;
}