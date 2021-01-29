namespace Fanex.Result.Core.GDG
{
    using System.Configuration;

    public sealed class GdgConfigs : ConfigurationSection
    {
        #region const
        private const string RESULT_CONFIG = "GDGResultConfigs";
        private const string URL = "Url";
        private const string VENDOR_ID = "VendorId";
        private const string DEBUG_MODE = "Debug";

        #endregion const       

        private static readonly GdgConfigs _configs = ConfigurationManager.GetSection(RESULT_CONFIG) as GdgConfigs;

        public static GdgConfigs Configs => _configs;

        [ConfigurationProperty(URL, IsRequired = true)]
        public string Url => this[URL].ToString();

        [ConfigurationProperty(VENDOR_ID, IsRequired = true)]
        public string VendorId => this[VENDOR_ID].ToString();

        [ConfigurationProperty(DEBUG_MODE, IsRequired = false, DefaultValue = 0)]
        public int DebugMode => (int)this[DEBUG_MODE];

    }
}