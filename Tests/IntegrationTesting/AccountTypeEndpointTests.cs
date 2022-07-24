using System.Diagnostics;
using System.Threading.Tasks;
using DataAccess.Models;
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
        Debugger.Break();
    }
}