namespace Fanex.Result.Core.Sportsbook.ViewModels.BasketBallDetail
{
    using BaseDetail;
    using System.Collections.Generic;

    public class BasketBallGameDetailItem : GameDetailItem
    {
        public bool IsHome { get; set; }

        public bool IsAway { get; set; }

        public BasketballPlayerInfos Players { get; set; }
    }
}