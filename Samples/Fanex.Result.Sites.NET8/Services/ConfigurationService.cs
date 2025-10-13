using Microsoft.Extensions.Configuration;

namespace Fanex.Result.Sites.NET8.Services;

public class ConfigurationService : IConfigurationService
{
    private readonly IConfiguration _configuration;

    public ConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString(string name)
    {
        return _configuration.GetConnectionString(name) ?? string.Empty;
    }

    public string GetAppSetting(string key)
    {
        return _configuration[$"AppSettings:{key}"] ?? string.Empty;
    }

    public T GetSection<T>(string sectionName) where T : class
    {
        return _configuration.GetSection(sectionName).Get<T>() ?? 
               throw new InvalidOperationException($"Configuration section '{sectionName}' not found or could not be bound to type {typeof(T).Name}");
    }
}
