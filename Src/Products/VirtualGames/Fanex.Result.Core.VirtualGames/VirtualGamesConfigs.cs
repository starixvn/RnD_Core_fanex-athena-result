namespace Fanex.Result.Core.VirtualGames
{
    using System.Configuration;

    public sealed class VirtualGamesConfigs : ConfigurationSection
    {
        #region const
        private const string __VIRTUALGAMES_RESULT_CONFIG = "VirtualGamesResultConfigs";
        private const string URLRESULT = "UrlResult";
        private const string URL_DETAIL_RESULT = "UrlDetailResult";
        private const string LOGIN = "Login";
        private const string PASSWORD = "Password";
        private const string SITE_ID = "SiteId";
        private const string DEBUG_MODE = "Debug";

        #endregion const       

        private static VirtualGamesConfigs _configs = ConfigurationManager.GetSection(__VIRTUALGAMES_RESULT_CONFIG) as VirtualGamesConfigs;

        public static VirtualGamesConfigs Configs
        {
            get
            {
                return _configs;
            }
        }

        [ConfigurationProperty(URLRESULT, IsRequired = true)]
        public string UrlResult
        {
            get { return this[URLRESULT].ToString(); }
            set { this[URLRESULT] = value; }
        }

        [ConfigurationProperty(URL_DETAIL_RESULT, IsRequired = true)]
        public string UrlDetailResult
        {
            get { return this[URL_DETAIL_RESULT].ToString(); }
            set { this[URL_DETAIL_RESULT] = value; }
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

        [ConfigurationProperty(SITE_ID, IsRequired = true)]
        public string SiteId
        {
            get { return this[SITE_ID].ToString(); }
            set { this[SITE_ID] = value; }
        }

        [ConfigurationProperty(DEBUG_MODE, IsRequired = false, DefaultValue = 0)]
        public int DebugMode
        {
            get { return (int)this[DEBUG_MODE]; }
            set { this[DEBUG_MODE] = value; }
        }
    }
}