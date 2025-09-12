namespace Fanex.Result.Sites.NET8.Services;

public interface IConfigurationService
{
    string GetConnectionString(string name);
    string GetAppSetting(string key);
    T GetSection<T>(string sectionName) where T : class;
}
