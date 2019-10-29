namespace Fanex.Result.Core.Casino
{
    using System.Configuration;

    public sealed class CasinoConfigs : ConfigurationSection
    {
        #region const

        private const string URL = "Url";
        private const string LOGIN = "Login";
        private const string PASSWORD = "Password";
        private const string SITE_ID = "SiteId";
        private const string VENDOR_ID = "VendorId";
        private const string DEBUG_MODE = "Debug";

        #endregion const       

        [ConfigurationProperty(URL, IsRequired = true)]
        public string Url
        {
            get { return this[URL].ToString(); }
            set { this[URL] = value; }
        }

        [ConfigurationProperty(LOGIN, IsRequired = true)]
        public string Login
        {
            get { return this[LOGIN].ToString(); }
            set { this[LOGIN] = value; }
        }

        [ConfigurationProperty(PASSWORD, IsRequired = true)]
        public string Password
        {
            get { return this[PASSWORD].ToString(); }
            set { this[PASSWORD] = value; }
        }

        [ConfigurationProperty(SITE_ID, IsRequired = false, DefaultValue = null)]
        public string SiteId
        {
            get { return this[SITE_ID].ToString(); }
            set { this[SITE_ID] = value; }
        }

        [ConfigurationProperty(VENDOR_ID, IsRequired = true)]
        public string VendorId
        {
            get { return this[VENDOR_ID].ToString(); }
            set { this[VENDOR_ID] = value; }
        }

        [ConfigurationProperty(DEBUG_MODE, IsRequired = false, DefaultValue = 0)]
        public int DebugMode
        {
            get { return (int)this[DEBUG_MODE]; }
            set { this[DEBUG_MODE] = value; }
        }
    }
}