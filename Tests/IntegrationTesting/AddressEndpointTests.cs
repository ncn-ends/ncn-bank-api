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
using Tests.Fixtures;
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
            zipcode = "11111",
            city = "San Francisco",
            state = "California",
            country = "United States",
            unit_number = 123,
            address_type = "Condo/Apartment"
        };
        var response = await client.SendPost<AddressDTO>(entry);
        response.EnsureSuccessStatusCode();

        var resBody = await JsonMapper.MapHttpContentAs<AddressInsertionReturnType>(response);
        resBody.Should().NotBeNull();
        resBody.address_id.Should().BeGreaterThan(0);
    }

    
    
    public static IEnumerable<object[]> AddAddress_TestData()
    {
        yield return new object[]
            {"123 Pierce St", "11111", "San Francisco", "California", "United States", 123, "Condo/Apartment"};
    }
    
    [Theory]
    [MemberData(nameof(AddAddress_TestData))]
    public async Task Post_AddAddress_Anonymous_MemberData(string street, string zipcode, string city, string state,
        string country, int unit_number, string address_type)
    {
        var client = new HttpClientBroker("/api/address");
        var entry = new AddressDTO
        {
            street = street,
            zipcode = zipcode,
            city = city,
            state = state,
            country = country,
            unit_number = unit_number,
            address_type = address_type
        };
        var response = await client.SendPost<AddressDTO>(entry);
        response.EnsureSuccessStatusCode();

        var resBody = await JsonMapper.MapHttpContentAs<AddressInsertionReturnType>(response);
        resBody.Should().NotBeNull();
        resBody.address_id.Should().BeGreaterThan(0);
    }
    
    [Theory]
    [ClassData(typeof(AddressTestData))]
    public async Task Post_AddAddress_Anonymous_ClassData(string street, string zipcode, string city, string state,
        string country, int unit_number, string address_type)
    {
        var client = new HttpClientBroker("/api/address");
        var entry = new AddressDTO
        {
            street = street,
            zipcode = zipcode,
            city = city,
            state = state,
            country = country,
            unit_number = unit_number,
            address_type = address_type
        };
        var response = await client.SendPost<AddressDTO>(entry);
        response.EnsureSuccessStatusCode();

        var resBody = await JsonMapper.MapHttpContentAs<AddressInsertionReturnType>(response);
        resBody.Should().NotBeNull();
        resBody.address_id.Should().BeGreaterThan(0);
    }
}