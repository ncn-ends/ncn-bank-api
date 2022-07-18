using System.Collections;
using System.Collections.Generic;
using Bogus;

namespace Tests.Fixtures;

public class AddressTestData : IEnumerable<object[]>
{
    private readonly Faker _faker = new();
    
    private object[] GenerateRandomizedDataset()
    {
        var streetName = _faker.Address.StreetName();
        var streetSuffix = _faker.Address.StreetSuffix();
        var houseNumber = _faker.Random.Number(1, 3000);
        var street = $"{houseNumber} {streetName} {streetSuffix}";
        var zipcode = _faker.Address.ZipCode();
        var city = _faker.Address.City();
        var state = _faker.Address.State();
        var country = "United States";
        var unit_number = _faker.Random.Number(1, 500);
        var address_type = "Condo/Apartment";

        return new object[]{street, zipcode, city, state, country, unit_number, address_type};
    }
    
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return GenerateRandomizedDataset();
        yield return GenerateRandomizedDataset();
        yield return GenerateRandomizedDataset();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}