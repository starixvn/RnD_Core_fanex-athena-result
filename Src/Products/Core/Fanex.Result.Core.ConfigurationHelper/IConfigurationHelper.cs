namespace Fanex.Result.Core.ConfigurationHelper
{
    public interface IConfigurationHelper
    {
        T GetValue<T>(string key, T defaultValue);

        object GetSection(string key);
    }
}