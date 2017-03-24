namespace Fanex.Result.Core.Sportsbook.ViewModels.BasketBallDetail
{
    using Base.Extension;

    public class BasketBallDetailViewModel
    {
        public BasketBallDetailViewModel(SafeList<BasketBallGameDetailItem> detailItems)
        {
            DetailItems = detailItems;
        }

        public SafeList<BasketBallGameDetailItem> DetailItems { get; }
    }
}