using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using DataAccess.Models;
using FluentAssertions;
using Tests.Helpers;
using Xunit;

namespace Tests.IntegrationTesting;

public class AccountTypeEndpointTests
{
    [Fact]
    public async Task  Get_GetAll()
    {
        var client = new HttpClientBroker("/api/accounttypes");
        var response = await client.SendGet<AccountTypeBO>();
        response.EnsureSuccessStatusCode();
        var content = await JsonMapper.MapHttpContentAs<List<AccountTypeBO>>(response);
        content.Should().HaveCount(7);
    }
}