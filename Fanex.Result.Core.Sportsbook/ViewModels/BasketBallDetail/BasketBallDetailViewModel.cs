namespace Fanex.Result.Core.Sportsbook.ViewModels.BasketBallDetail
{
    using System.Collections.Generic;

    public class BasketBallDetailViewModel
    {
        public BasketBallDetailViewModel(IList<BasketBallGameDetailItem> detailItems)
        {
            DetailItems = detailItems;
        }

        public IList<BasketBallGameDetailItem> DetailItems { get; }
    }
}