namespace Fanex.Result.Core.Sportsbook.ViewModels.ESoccer
{
    using Base;
    using Base.DataDomain;
    using Base.Resources;
    using BaseDetail;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using Utilities;

    public class ESoccerResultDetail : BaseResultDetail<ESoccerGameDetailItem>
    {
        public ESoccerResultDetail(
            Sportsbook sportsbook,
            ResultSetting setting,
            IRefDataHelper refDataHelper,
            ISportsbookHelperWraper sportsBookHelper)
            : base(sportsbook, setting, refDataHelper, sportsBookHelper)
        {
        }

        protected override ESoccerGameDetailItem CreateGameDetailItem(GameDetail detailItem)
        {
            var item = base.CreateGameDetailItem(detailItem);

            return MapGameValueByStatusId(item, detailItem);
        }

        protected override ESoccerGameDetailItem MapGameValueByStatusId(ESoccerGameDetailItem item, GameDetail detailItem)
        {
            switch (detailItem.SettlementStatusID)
            {
                case EventStatusID.CompletedSettlementStatusID:
                case EventStatusID.RefundWithSettleDetermined:
                    MapGameValueByPattern(item, detailItem);
                    break;
                
                case EventStatusID.RefundSettlementStatusID:
                    item.HomeValue = LangResource.Refund;
                    item.AwayValue = LangResource.Refund;
                    break;              

                default:
                    item.HomeValue = ConstantValue.Hyphen;
                    item.AwayValue = ConstantValue.Hyphen;
                    break;
            }

            item.HomeValue = string.IsNullOrEmpty(item.HomeValue) ? "-" : item.HomeValue;
            item.AwayValue = string.IsNullOrEmpty(item.AwayValue) ? "-" : item.AwayValue;

            return item;
        }
    }
}