using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Tests.Helpers;
using Xunit;

namespace Tests.IntegrationTesting;

public class AddressEndpointTests
{
    [Fact]
    public async Task Post_AddAddress_Anonymous()
    {
        var client = new HttpClientBroker("/api/address");
        var entry = new AddressDTO
        {
            street = "123 Pierce St",
            zipcode=  "11111",
            city = "San Francisco",
            state = "California",
            country = "United States",
            unit_number = 123,
            address_type = "Condo/Apartment"
        };
        var response = await client.SendPost<AddressDTO>(entry);
        response.EnsureSuccessStatusCode();

        var resBody = await JsonMapper.MapHttpContentAs<AddressDTO>(response);
        resBody.Should().NotBeNull();

        var areEqual = Comparisons.AreEqual<AddressDTO>(resBody, entry);
        areEqual.Should().BeTrue();

    }
}