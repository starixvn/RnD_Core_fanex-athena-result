namespace Fanex.Result.Core.Sportsbook.ViewModels.SoccerInfo
{
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;

    public class SoccerGameDetailItem : IGameDetailItem
    {
        public string GroupCode { get; set; }

        public string GroupName { get; set; }

        public string ResourceName { get; set; }

        public string ResourceID { get; set; }

        public string HomeValue { get; set; }

        public string AwayValue { get; set; }

        public int OrderNo { get; set; }
    }
}