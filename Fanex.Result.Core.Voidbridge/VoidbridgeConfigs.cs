namespace Fanex.Result.Core.Voidbridge
{
    using System.Configuration;

    internal sealed class VoidbridgeConfigs : ConfigurationSection
    {
        #region const

        private const string __VOIDBRIDGE_RESULT_CONFIG = "VoidbridgeResultConfigs";
        private const string __URL = "Url";
        private const string __LOGIN = "Login";
        private const string __PASSWORD = "Password";
        private const string __SITE_ID = "SiteId";
        private const string __VENDOR_ID = "VendorId";
        private const string __DEBUG_MODE = "Debug";

        #endregion const

        private static VoidbridgeConfigs _configs = ConfigurationManager.GetSection(__VOIDBRIDGE_RESULT_CONFIG) as VoidbridgeConfigs;

        public static VoidbridgeConfigs Configs
        {
            get
            {
                return _configs;
            }
        }

        [ConfigurationProperty(__URL, IsRequired = true)]
        public string Url
        {
            get { return this[__URL].ToString(); }
            set { this[__URL] = value; }
        }

        [ConfigurationProperty(__LOGIN, IsRequired = true)]
        public string Login
        {
            get { return this[__LOGIN].ToString(); }
            set { this[__LOGIN] = value; }
        }

        [ConfigurationProperty(__PASSWORD, IsRequired = true)]
        public string Password
        {
            get { return this[__PASSWORD].ToString(); }
            set { this[__PASSWORD] = value; }
        }

        [ConfigurationProperty(__SITE_ID, IsRequired = true)]
        public string SiteId
        {
            get { return this[__SITE_ID].ToString(); }
            set { this[__SITE_ID] = value; }
        }

        [ConfigurationProperty(__VENDOR_ID, IsRequired = true)]
        public string VendorId
        {
            get { return this[__VENDOR_ID].ToString(); }
            set { this[__VENDOR_ID] = value; }
        }

        [ConfigurationProperty(__DEBUG_MODE, IsRequired = false, DefaultValue = 0)]
        public int DebugMode
        {
            get { return (int)this[__DEBUG_MODE]; }
            set { this[__DEBUG_MODE] = value; }
        }
    }
}