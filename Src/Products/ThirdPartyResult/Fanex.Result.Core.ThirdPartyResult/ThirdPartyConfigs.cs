namespace Fanex.Result.Core.ThirdParty
{
    using System.Configuration;

    public sealed class ThirdPartyConfigs : ConfigurationSection
    {
        public ThirdPartyConfigs(string url, int debugMode, int vendorId, string siteId, string proxyIP, int proxyPort)
        {
            Url = url;
            DebugMode = debugMode;
            VendorId = vendorId;
            SiteId = siteId;
            ProxyIP = proxyIP;
            ProxyPort = proxyPort;
        }

        #region const
        private const string URL = "Url";
        private const string DEBUG_MODE = "Debug";
        private const string VENDORID = "VendorId";
        private const string SITEID = "SiteId";
        private const string PROXY_IP_SERVER = "ProxyIP";
        private const string PROXY_PORT_SERVER = "ProxyPort";
        #endregion const       

        [ConfigurationProperty(URL, IsRequired = true)]
        public string Url
        {
            get
            {
                return this[URL].ToString();
            }
            set
            {
                this[URL] = value;
            }
        }

        [ConfigurationProperty(DEBUG_MODE, IsRequired = false, DefaultValue = 0)]
        public int DebugMode
        {
            get
            {
                return (int)this[DEBUG_MODE];
            }
            set
            {
                this[DEBUG_MODE] = value;
            }
        }

        [ConfigurationProperty(VENDORID, IsRequired = false, DefaultValue = 0)]
        public int VendorId
        {
            get
            {
                return (int)this[VENDORID];
            }
            set
            {
                this[VENDORID] = value;
            }
        }

        [ConfigurationProperty(SITEID, IsRequired = false, DefaultValue = "")]
        public string SiteId
        {
            get
            {
                return this[SITEID].ToString();
            }
            set
            {
                this[SITEID] = value;
            }
        }

        [ConfigurationProperty(PROXY_IP_SERVER, IsRequired = false, DefaultValue = "")]
        public string ProxyIP
        {
            get
            {
                return this[PROXY_IP_SERVER].ToString();
            }

            set
            {
                this[PROXY_IP_SERVER] = value;
            }
        }

        [ConfigurationProperty(PROXY_PORT_SERVER, IsRequired = false, DefaultValue = 0)]
        public int ProxyPort
        {
            get
            {
                return (int)this[PROXY_PORT_SERVER];
            }
            set
            {
                this[PROXY_PORT_SERVER] = value;
            }
        }
    }
}