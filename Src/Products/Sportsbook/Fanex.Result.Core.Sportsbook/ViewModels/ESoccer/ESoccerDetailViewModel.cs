namespace Fanex.Result.Core.Sportsbook.ViewModels.ESoccer
{
    using Base.Extension;

    public class ESoccerDetailViewModel
    {
        public ESoccerDetailViewModel(SafeList<ESoccerGameDetailItem> detailItems)
        {
            DetailItems = detailItems;
        }

        public SafeList<ESoccerGameDetailItem> DetailItems { get; }
    }
}