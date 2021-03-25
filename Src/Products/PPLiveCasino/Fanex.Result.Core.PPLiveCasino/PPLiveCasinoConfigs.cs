namespace Fanex.Result.Core.PPLiveCasino
{
    using System.Configuration;

    public sealed class PPLiveCasinoConfigs : ConfigurationSection
    {
        #region const
        private const string RESULT_CONFIG = "PPLiveCasinoResultConfigs";
        private const string URL = "Url";
        private const string SITE_ID = "SiteId";
        private const string VENDOR_ID = "VendorId";
        private const string DEBUG_MODE = "Debug";
        private const string PROXY_IP = "ProxyIP";
        private const string PROXY_PORT = "ProxyPort";

        #endregion const       

        private static readonly PPLiveCasinoConfigs _configs = ConfigurationManager.GetSection(RESULT_CONFIG) as PPLiveCasinoConfigs;

        public static PPLiveCasinoConfigs Configs => _configs;

        [ConfigurationProperty(URL, IsRequired = true)]
        public string Url => this[URL].ToString();

        [ConfigurationProperty(SITE_ID, IsRequired = false, DefaultValue = null)]
        public string SiteId => this[SITE_ID].ToString();

        [ConfigurationProperty(VENDOR_ID, IsRequired = true)]
        public string VendorId => this[VENDOR_ID].ToString();

        [ConfigurationProperty(DEBUG_MODE, IsRequired = false, DefaultValue = 0)]
        public int DebugMode => (int)this[DEBUG_MODE];

        [ConfigurationProperty(PROXY_IP, IsRequired = false, DefaultValue = "")]
        public string ProxyIP => this[PROXY_IP].ToString();

        [ConfigurationProperty(PROXY_PORT, IsRequired = false, DefaultValue = 0)]
        public int ProxyPort => (int)this[PROXY_PORT];
    }
}