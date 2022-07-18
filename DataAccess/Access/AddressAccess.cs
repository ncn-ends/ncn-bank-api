using System.Data;
using DataAccess.Models;

namespace DataAccess.Access;

public interface IAddressAccess
{
    Task<string> AddAddress(AddressDTO addressDetails);
}

public class AddressAccess : IAddressAccess
{
    private readonly ISqlDataAccess _db;

    public AddressAccess(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<string> AddAddress(AddressDTO addressDetails)
    {
        var queryResult = await _db.CallUdf<AddressInsertionReturnType, dynamic>("SR_Address_Insert", new
        {
            _street = addressDetails.street,
            _zipcode = addressDetails.zipcode,
            _city = addressDetails.city,
            _state = addressDetails.state,
            _country = addressDetails.country,
            _unit_number = addressDetails.unit_number,
            _address_type = addressDetails.address_type
        });

        var row = queryResult.FirstOrDefault();
        if (row is null) throw new DataException();
        
        return row.address_id;
    }
}