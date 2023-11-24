namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using Base;
    using Base.DataDomain;
    using Base.ViewModels;
    using BaseDetail;
    using BasketBallDetail;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using System.Linq;

    public sealed class FootballViewModel : BasketBallViewModel, IResultViewModel
    {
        public FootballViewModel(
            Sportsbook sportbook,
            ResultSetting setting,
            IResultDetail<BasketBallGameDetailItem> resultDetail = null)
            : this(sportbook, setting, resultDetail, null)
        {
            IsShowLiveScore = false;
        }

        public FootballViewModel(
          Sportsbook sportbook,
          ResultSetting setting,
          IResultDetail<BasketBallGameDetailItem> resultDetail,
          IRefDataHelper refDataHelper)
            : base(sportbook, setting, resultDetail, refDataHelper)
        {
            IsShowLiveScore = false;
        }

        public override string GetResultFromGameDetails(string resourceId, bool isHome)
        {
            var result = ResultDetailItems.FirstOrDefault(g => g.ResourceID == resourceId);

            return isHome ? result?.HomeValue : result?.AwayValue;
        }
    }
}