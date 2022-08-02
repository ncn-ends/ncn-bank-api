using System.Data;
using DataAccess.Models;

namespace DataAccess.Access;

public interface IAddressAccess
{
    Task<int> AddAddress(AddressDTO addressDetails);
    Task<IEnumerable<AddressBO>> GetAllByAccountHolder(Guid holderId);
}

public class AddressAccess : IAddressAccess
{
    private readonly ISqlDataAccess _db;
    private readonly ISqlDataAccess _dataAccess;

    public AddressAccess(ISqlDataAccess db, ISqlDataAccess dataAccess)
    {
        _db = db;
        _dataAccess = dataAccess;
    }

    public async Task<int> AddAddress(AddressDTO addressDetails)
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

    public async Task<IEnumerable<AddressBO>> GetAllByAccountHolder(Guid holderId)
    {
        var udfName = "SR_Addresses_GetAllByAccountHolder";
        var udfParams = new
        {
            _account_holder_id = holderId
        };
        var addresses = await _dataAccess.CallUdf<AddressBO, dynamic>(udfName, udfParams);
        
        return addresses;
    }
}