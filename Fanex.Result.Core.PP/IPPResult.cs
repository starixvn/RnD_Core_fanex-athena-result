namespace Fanex.Result.Core.PP
{
    public interface IPPResult
    {
        string GetPPResult(string custId, string gamePlayId, string language = "en-US", string siteId = null);
    }
}