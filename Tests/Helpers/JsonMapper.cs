using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using DataAccess.Models;
using Newtonsoft.Json;

namespace Tests.Helpers;

public static class JsonMapper
{
    public static async Task<T> MapHttpContentAs<T>(HttpResponseMessage res)
    {
        var resultString = await res.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(resultString);
    }
}