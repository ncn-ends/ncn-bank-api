using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Tests.Helpers;

public class HttpClientBroker
{
    private readonly HttpClient _client;
    private readonly string _endpoint;
    
    public HttpClientBroker(string endpoint, HttpClient? injectedClient = null)
    {
        if (injectedClient is null)
        {
            var waf = new CustomWAF<Program>();
            _client = waf.CreateClient();
        }
        else
        {
            _client = injectedClient;
        }
        
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
    
    public async Task<HttpResponseMessage> SendGet(string route = "")
    {
        return await _client.GetAsync(_endpoint + route);
    }
}