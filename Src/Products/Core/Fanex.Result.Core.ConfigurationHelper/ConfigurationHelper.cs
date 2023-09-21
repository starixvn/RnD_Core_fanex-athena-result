using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Specialized;

namespace Fanex.Result.Core.ConfigurationHelper
{
    public class ConfigurationHelper : IConfigurationHelper
    {
        public T GetValue<T>(string key, T defaultValue)
        {
            try
            {
                var value = System.Configuration.ConfigurationManager.AppSettings[key];
                if (value == null)
                {
                    return defaultValue;
                }

                TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(T));

                return (T)typeConverter.ConvertFromString(value);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Key: {key}", ex);
            }
        }

        public object GetSection(string key)
        => ConfigurationManager.GetSection(key);
    }
}