namespace Fanex.Result.Core.Habanero
{
    using System.Configuration;

    public sealed class HabaneroConfigs : ConfigurationSection
    {
        #region const
        private const string URL = "Url";
        private const string DEBUG_MODE = "Debug";

        #endregion const       

        [ConfigurationProperty(URL, IsRequired = true)]
        public string Url => this[URL].ToString();

        [ConfigurationProperty(DEBUG_MODE, IsRequired = false, DefaultValue = 0)]
        public int DebugMode => (int)this[DEBUG_MODE];

    }
}