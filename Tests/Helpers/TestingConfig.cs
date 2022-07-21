using System.Reflection;
using Microsoft.Extensions.Configuration;
using IConfiguration = Castle.Core.Configuration.IConfiguration;

namespace Tests.Helpers;

static public class TestingConfig
{
    public static IConfigurationRoot GetConfig()
    {
        return new ConfigurationBuilder()
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .AddEnvironmentVariables()
            .Build();
    }  
}