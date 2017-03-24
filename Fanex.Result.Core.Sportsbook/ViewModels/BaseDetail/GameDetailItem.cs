namespace Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail
{
    public class GameDetailItem : IGameDetailItem
    {
        public string ResourceName { get; set; }

        public string ResourceID { get; set; }

        public string HomeValue { get; set; }

        public string AwayValue { get; set; }
    }
}