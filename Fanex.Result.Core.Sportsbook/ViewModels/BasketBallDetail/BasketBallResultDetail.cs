namespace Fanex.Result.Core.Sportsbook.ViewModels.BasketBallDetail
{
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using Base.Resources;
    using BaseDetail;
    using DataDomain;
    using DTO;
    using Utilities;

    public class BasketBallResultDetail : BaseResultDetail<BasketBallGameDetailItem>
    {
        public BasketBallResultDetail(
            Sportsbook sportsbook,
            ResultSetting setting,
            ISportsbookRefDataHelper refDataHelper,
            ISportsbookHelperWraper sportsBookHelper)
            : base(sportsbook, setting, refDataHelper, sportsBookHelper)
        {
        }

        protected override BasketBallGameDetailItem CreateGameDetailItem(GameDetail detailItem)
        {
            var item = base.CreateGameDetailItem(detailItem);

            if (BasketBallResourceID.MoreResultResourceIDs.Contains(detailItem.ResourceId))
            {
                var betTypeId = detailItem?.ResourceId?.Substring(0, 3);
                var resourceId = detailItem?.ResourceId?.Remove(0, 3);

                item.ResourceName = SportsbookRefDataHelper.GetBetTypeNameById(betTypeId, GetSupportedLanguage, resourceId);
            }

            return MapGameValueByStatusId(item, detailItem);
        }

        protected override BasketBallGameDetailItem MapGameValueByStatusId(BasketBallGameDetailItem item, GameDetail detailItem)
        {
            item = base.MapGameValueByStatusId(item, detailItem);

            item.HomeValue = string.IsNullOrEmpty(item.HomeValue) ? "-" : item.HomeValue;
            item.AwayValue = string.IsNullOrEmpty(item.AwayValue) ? "-" : item.AwayValue;

            if (detailItem.SettlementStatusID == EventStatusID.RunningSettlementStatusID)
            {
                item.HomeValue = ConstantValue.Hyphen;
                item.AwayValue = ConstantValue.Hyphen;
            }

            return item;
        }

        protected override void MapGameValueByPattern(BasketBallGameDetailItem item, GameDetail detailItem)
        {
            base.MapGameValueByPattern(item, detailItem);

            if (BasketBallResourceID.MoreResultResourceIDs.Contains(detailItem.ResourceId))
            {
                MapGameValueForMoreResult(item, detailItem);
            }
        }

        private void MapGameValueForMoreResult(BasketBallGameDetailItem item, GameDetail detailItem)
        {
            var isHomeTeam = detailItem?.HomeValue?.ToLowerInvariant() == "h";
            var isNone = detailItem?.HomeValue?.ToLowerInvariant() == "n";

            var result = isHomeTeam ? HomeTeam : AwayTeam;

            item.IsHome = isNone ? false : isHomeTeam;
            item.IsAway = isNone ? false : !isHomeTeam;
            item.HomeValue = isNone ? LangResource.None : result;
        }
    }
}