namespace Fanex.Result.Core.Voidbridge
{
    public interface VoidbridgeApi
    {
#pragma warning disable S3995 // URI return values should not be strings

        string GetBetHistoryUrl(string custId, string gamePlayId, string language = "en-US", string siteId = null);

#pragma warning restore S3995 // URI return values should not be strings
    }
}