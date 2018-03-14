namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Core.Sportsbook.Utilities;
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;
    using Fanex.Result.Core.Sportsbook.ViewModels.BasketBallDetail;
    using Fanex.Result.Core.Sportsbook.ViewModels.VSBasketBallDetail;

    public class VSBasketBallViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public VSBasketBallViewModel(Sportsbook soccer, ResultSetting setting)
           : this(soccer, setting, null, null)
        {
        }

        public VSBasketBallViewModel(
            Sportsbook sportbook,
            ResultSetting setting,
            ISportsbookRefDataHelper refDataHelper,
            IResultDetail<BasketBallGameDetailItem> resultDetail)
            : base(sportbook, setting, refDataHelper)
        {
            ResultDetailItems = (resultDetail ?? new VSBasketBallResultDetail(sportbook, setting)).GetDetailItems();
            MoreResultDetailItems = ResultDetailItems.Where(item =>
                item.ResourceID != GameResourceID.FirstHalfScore &&
                item.ResourceID != GameResourceID.FinalScore).ToSafeList();

            sportbook.MatchCode = SportsbookRefDataHelper.GetBetRadarMatchCodeFormat(sportbook.MatchCode, GetSupportedLanguage);
        }

        public SafeList<BasketBallGameDetailItem> ResultDetailItems { get; set; }

        public SafeList<BasketBallGameDetailItem> MoreResultDetailItems { get; set; }

        public string HTHomeScore => ResultDetailItems?.FirstOrDefault(item => item.ResourceID == GameResourceID.FirstHalfScore)?.HomeValue;

        public string HTAwayScore => ResultDetailItems?.FirstOrDefault(item => item.ResourceID == GameResourceID.FirstHalfScore)?.AwayValue;

        public string FinalAwayScore => ResultDetailItems?.FirstOrDefault(item => item.ResourceID == GameResourceID.FinalScore)?.AwayValue;

        public string FinalHomeScore => ResultDetailItems?.FirstOrDefault(item => item.ResourceID == GameResourceID.FinalScore)?.HomeValue;

        public string MatchInfo => $"({SPORTBOOK.MatchCode})";

        public override bool VisibleDetailedLink => base.VisibleDetailedLink && HasValidResultDetail;

        public override bool VisibleGameDetail => HasValidResultDetail;

        public int HomeId => SPORTBOOK.HomeId;

        public int AwayId => SPORTBOOK.AwayId;

        private bool HasValidResultDetail
            => SPORTBOOK.GameDetails.Any(item =>
                VSBasketBallResourceID.MoreResultResourceIDs.Contains(item.ResourceId) &&
                item.SettlementStatusID != EventStatusID.RunningSettlementStatusID);
    }
}