namespace Fanex.Result.Core.GlobalGaming
{
    using System.Configuration;

    internal sealed class GlobalGamingConfigs : ConfigurationSection
    {
        #region const

        private const string __GLOBAL_GAMING_RESULT_CONFIG = "GlobalGamingResultConfigs";
        private const string __URL = "Url";
        private const string __LOGIN = "Login";
        private const string __PASSWORD = "Password";
        private const string __SITE_ID = "SiteId";
        private const string __STYLE = "Style";
        private const string __DEBUG_MODE = "Debug";

        #endregion const

        public static GlobalGamingConfigs Configs
        {
            get
            {
                return ConfigurationManager.GetSection(__GLOBAL_GAMING_RESULT_CONFIG) as GlobalGamingConfigs;
            }
        }

        [ConfigurationProperty(__URL, IsRequired = true)]
#pragma warning disable S3996 // URI properties should not be strings
        public string Url
#pragma warning restore S3996 // URI properties should not be strings
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

        [ConfigurationProperty(__SITE_ID, IsRequired = false, DefaultValue = null)]
        public string SiteId
        {
            get { return this[__SITE_ID].ToString(); }
            set { this[__SITE_ID] = value; }
        }

        [ConfigurationProperty(__STYLE, IsRequired = false, DefaultValue = "0")]
        public string Style
        {
            get { return this[__STYLE].ToString(); }
            set { this[__STYLE] = value; }
        }

        [ConfigurationProperty(__DEBUG_MODE, IsRequired = false, DefaultValue = 0)]
        public int DebugMode
        {
            get { return (int)this[__DEBUG_MODE]; }
            set { this[__DEBUG_MODE] = value; }
        }
    }
}