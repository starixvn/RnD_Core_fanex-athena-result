namespace Fanex.Result.Core.Casino
{
    public interface ICasinoResult
    {
        string GetCasinoResult(string custId, string gamePlayId, string language = "en-US", string siteId = null);
    }
}