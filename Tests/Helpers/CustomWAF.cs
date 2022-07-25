using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests.Helpers;

// TODO: ask about why we cant just pass Program to this generic directly
public class CustomWAF<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(Services =>
        {
            
        });
    }
    
}