namespace Fanex.Result.Core.Sportsbook.ViewModels.BasketBallDetail
{
    using BaseDetail;

    public class BasketBallGameDetailItem : GameDetailItem
    {
        public bool IsHome { get; set; }

        public bool IsAway { get; set; }

        public BasketballPlayerInfos Players { get; set; }

        public int SettlementStatusID { get; set; }
        
        public bool IsRefundAll { get; set; }

        public bool IsRefundWithSettleDetermined { get; set; }
    }
}