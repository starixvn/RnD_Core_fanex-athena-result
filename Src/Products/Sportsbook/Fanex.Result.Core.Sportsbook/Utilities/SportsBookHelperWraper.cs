namespace Fanex.Result.Core.Sportsbook.Utilities
{
    public class SportsbookHelperWraper : ISportsbookHelperWraper
    {
        public string GetResourceLang(string keyName)
        {
            return SportsbookHelper.GetResourceLang(keyName);
        }
    }
}