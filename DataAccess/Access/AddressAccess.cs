using System.Data;
using System.Diagnostics;
using DataAccess.Models;

namespace DataAccess.Access;

public interface IAddressAccess
{
    Task<Guid> AddAddress(AddressInsertionForm addressForm);
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

    public async Task<Guid> AddAddress(AddressInsertionForm addressForm)
    {
        var queryResult = await _db.CallUdf<AddressInsertionReturnType, dynamic>("SR_Address_Insert", new
        {
            _street = addressForm.street,
            _zipcode = addressForm.zipcode,
            _city = addressForm.city,
            _state = addressForm.state,
            _country = addressForm.country,
            _unit_number = addressForm.unit_number,
            _address_type = addressForm.address_type,
            _account_holder_id = addressForm.account_holder_id
        });
        Debugger.Break();
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