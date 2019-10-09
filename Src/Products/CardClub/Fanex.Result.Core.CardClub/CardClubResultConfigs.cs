namespace Fanex.Result.Core.CardClub
{
    using System.Configuration;

    public interface ICardClubResultConfigs
    {
#pragma warning disable S3996 // URI properties should not be strings
        string Url { get; set; }
#pragma warning restore S3996 // URI properties should not be strings

        string Login { get; set; }

        string Password { get; set; }

        int SiteId { get; set; }

        int DebugMode { get; set; }
    }

    public sealed class CardClubResultConfigs : ConfigurationSection, ICardClubResultConfigs
    {
        #region const

        private const string RESULT_CONFIG = "CardClubResultConfigs";
        private const string URL = "Url";
        private const string LOGIN = "Login";
        private const string PASSWORD = "Password";
        private const string SITE_ID = "SiteId";
        private const string DEBUG_MODE = "Debug";

        #endregion const

        public static readonly CardClubResultConfigs Configs = ConfigurationManager.GetSection(RESULT_CONFIG) as CardClubResultConfigs;

        [ConfigurationProperty(URL, IsRequired = true)]
#pragma warning disable S3996 // URI properties should not be strings
        public string Url
        {
            get { return this[URL].ToString(); }
            set { this[URL] = value; }
        }

#pragma warning restore S3996 // URI properties should not be strings

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

        [ConfigurationProperty(SITE_ID, IsRequired = false, DefaultValue = 0)]
        public int SiteId
        {
            get { return (int)this[SITE_ID]; }
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