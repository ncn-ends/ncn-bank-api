using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Tests.Helpers;

public class HttpClientBroker
{
    private readonly WebApplicationFactory<Program> _waf = new();
    private readonly HttpClient _client;
    private readonly string _endpoint;
    
    public HttpClientBroker(string endpoint)
    {
        _client = _waf.CreateClient();
        
        _endpoint = endpoint;
        
        _client.DefaultRequestHeaders
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    
    public async Task<HttpResponseMessage> SendPost<T>(T entry)
    {
        var serializedEntry = JsonConvert.SerializeObject(entry);
        var body = new StringContent(serializedEntry, Encoding.UTF8, "application/json");
        
        return await _client.PostAsync(_endpoint, body);
    }
    
    public async Task<HttpResponseMessage> SendGet<T>()
    {
        return await _client.GetAsync(_endpoint);
    }
}