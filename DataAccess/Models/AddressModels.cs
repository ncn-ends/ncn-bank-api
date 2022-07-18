namespace DataAccess.Models;

public class AddressBO
{
    
}

public class AddressDTO
{
    public string street { get; set; } = "UNSET STREET ADDRESS";
    public string zipcode { get; set; } = "UNSET ZIPCODE";
    public string city { get; set; } = "UNSET CITY";
    public string state { get; set; } = "UNSET STATE";
    public string country { get; set; } = "UNSET COUNTRY";
    public int unit_number { get; set; } = -1;
    public string address_type { get; set; } = "UNSET ADDRESS TYPE";
}

public class AddressInsertionReturnType
{
    public int address_id { get; set; } = -1;
}