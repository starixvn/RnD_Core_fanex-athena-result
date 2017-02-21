namespace Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail
{
    public interface IGameDetailItem
    {
        string ResourceName { get; set; }

        string ResourceID { get; set; }

        string HomeValue { get; set; }

        string AwayValue { get; set; }
    }
}