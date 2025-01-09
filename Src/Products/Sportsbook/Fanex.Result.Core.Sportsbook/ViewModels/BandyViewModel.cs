namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Core.Sportsbook.ViewModels.BandyDetail;
    using Fanex.Result.Core.Sportsbook.ViewModels.SoccerDetail;

    public sealed class BandyViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public BandyViewModel(Sportsbook sportbook, ResultSetting setting)
            : this(sportbook, setting, null)
        {
        }

        public BandyViewModel(
            Sportsbook sportbook,
            ResultSetting setting,
            IRefDataHelper refDataHelper)
            : base(sportbook, setting, refDataHelper)
        {
            ResultDetailItems = new BandyResultDetail(sportbook, setting).GetDetailItems();
        }

        public SafeList<SoccerGameDetailItem> ResultDetailItems { get; set; }

        public string HTHomeScore => Score(GameResourceID.FirstHalfScore, isHome: true);

        public string HTAwayScore => Score(GameResourceID.FirstHalfScore, isHome: false);

        public string FinalAwayScore => Score(GameResourceID.FinalScore, isHome: false);

        public string FinalHomeScore => Score(GameResourceID.FinalScore, isHome: true);

        private string Score(string resourceId, bool isHome)
        {
            var item = ResultDetailItems.FirstOrDefault(i => i.ResourceID == resourceId);
            if (item == null)
            {
                return ConstantValue.Hyphen;
            }

            return isHome ? item.HomeValue : item.AwayValue;
        }
    }
}