namespace Fanex.Result.Core.SabaClub
{
    using System.Configuration;

    public sealed class SabaClubConfigs : ConfigurationSection
    {
        #region const

        private const string URL = "Url";
        private const string LOGIN = "Login";
        private const string PASSWORD = "Password";
        private const string DEBUG_MODE = "Debug";

        #endregion const       

        public static readonly SabaClubConfigs Configs = ConfigurationManager.GetSection("SabaClubConfigs") as SabaClubConfigs;

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

        [ConfigurationProperty(DEBUG_MODE, IsRequired = false, DefaultValue = 0)]
        public int DebugMode
        {
            get { return (int)this[DEBUG_MODE]; }
            set { this[DEBUG_MODE] = value; }
        }
    }
}