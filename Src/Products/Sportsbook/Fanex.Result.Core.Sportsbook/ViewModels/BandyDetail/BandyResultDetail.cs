namespace Fanex.Result.Core.Sportsbook.ViewModels.BandyDetail
{
    using Base;
    using BaseDetail;
    using DTO;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Sportsbook.ViewModels.SoccerDetail;
    using Utilities;

    public class BandyResultDetail : BaseResultDetail<SoccerGameDetailItem>
    {
        public BandyResultDetail(
            Sportsbook sportsbook,
            ResultSetting setting,
            IRefDataHelper refDataHelper = null,
            ISportsbookHelperWraper sportsBookHelper = null)
            : base(sportsbook, setting, refDataHelper, sportsBookHelper)
        {
        }

        protected override SoccerGameDetailItem MapGameValueByStatusId(SoccerGameDetailItem item, GameDetail detailItem)
        {
            var detail = base.MapGameValueByStatusId(item, detailItem);

            if (detailItem.SettlementStatusID == EventStatusID.RunningSettlementStatusID)
            {
                detail.HomeValue = detail.AwayValue = ConstantValue.Hyphen;
            }

            detail.HomeValue = string.IsNullOrEmpty(detail.HomeValue) ? ConstantValue.Hyphen : detail.HomeValue;
            detail.AwayValue = string.IsNullOrEmpty(detail.AwayValue) ? ConstantValue.Hyphen : detail.AwayValue;
            
            return detail;
        }
    }
}