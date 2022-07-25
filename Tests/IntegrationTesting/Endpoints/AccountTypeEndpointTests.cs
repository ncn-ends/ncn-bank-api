using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Models;
using FluentAssertions;
using Tests.Helpers;
using Xunit;

namespace Tests.IntegrationTesting.Endpoints;

[Collection("SequentialTesting")]
public class AccountTypeEndpointTests
{
    [Fact]
    public async Task  Get_GetAll()
    {
        var client = new HttpClientBroker("/api/accounttypes");
        var response = await client.SendGet();
        response.EnsureSuccessStatusCode();
        var content = await JsonMapper.MapHttpContentAs<List<AccountTypeBO>>(response);
        content.Should().HaveCount(7);
    }
}