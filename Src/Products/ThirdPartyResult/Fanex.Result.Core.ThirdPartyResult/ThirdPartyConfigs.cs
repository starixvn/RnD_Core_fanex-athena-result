namespace Fanex.Result.Core.ThirdParty
{
    using System.Configuration;

    public sealed class ThirdPartyConfigs : ConfigurationSection
    {
        #region const
        private const string URL = "Url";
        private const string DEBUG_MODE = "Debug";
        private const string VENDORID = "VendorId";
        private const string SITEID = "SiteId";
        private const string PROXY_IP_SERVER = "ProxyIP";
        private const string PROXY_PORT_SERVER = "ProxyPort";
        #endregion const       

        [ConfigurationProperty(URL, IsRequired = true)]
        public string Url => this[URL].ToString();

        [ConfigurationProperty(DEBUG_MODE, IsRequired = false, DefaultValue = 0)]
        public int DebugMode => (int)this[DEBUG_MODE];

        [ConfigurationProperty(VENDORID, IsRequired = false, DefaultValue = 0)]
        public int VendorId => (int)this[VENDORID];

        [ConfigurationProperty(SITEID, IsRequired = false, DefaultValue = "0")]
        public string SiteId => this[SITEID].ToString();

        [ConfigurationProperty(PROXY_IP_SERVER, IsRequired = false, DefaultValue = "")]
        public string ProxyIP => this[PROXY_IP_SERVER].ToString();

        [ConfigurationProperty(PROXY_PORT_SERVER, IsRequired = false, DefaultValue = 0)]
        public int ProxyPort => (int)this[PROXY_PORT_SERVER];
    }
}