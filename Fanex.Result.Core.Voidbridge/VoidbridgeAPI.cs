namespace Fanex.Result.Core.Voidbridge
{
    public interface VoidbridgeApi
    {
        string GetBetHistoryUrl(string custId, string gamePlayId);

        string RenderBetHistory(string custId, string gamePlayId);
    }
}