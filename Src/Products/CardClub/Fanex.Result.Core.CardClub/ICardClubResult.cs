namespace Fanex.Result.Core.CardClub
{
    public interface ICardClubResult
    {
        string GetResult(string custId, string gameTypeId, string transId3rd, string language = "en-US", int siteId = 0);
    }
}
