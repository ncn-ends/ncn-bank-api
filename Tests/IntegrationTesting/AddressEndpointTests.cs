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
        await using var app = new WebApplicationFactory<Program>();
        using var client = app.CreateClient();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

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
        var serializedEntry = JsonConvert.SerializeObject(entry);
        var body = new StringContent(serializedEntry, Encoding.UTF8, "application/json");
        var endpoint = "/api/address";
        
        var response = await client.PostAsync(endpoint, body);
        
        response.EnsureSuccessStatusCode();

        var resultString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<AddressDTO>(resultString);
        result.Should().NotBeNull();

        var areEqual = Comparisons.AreEqual<AddressDTO>(result, entry);
        areEqual.Should().BeTrue();

    }
}