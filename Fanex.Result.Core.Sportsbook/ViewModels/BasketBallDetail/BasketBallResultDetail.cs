namespace Fanex.Result.Core.Sportsbook.ViewModels.BasketBallDetail
{
    using System.Linq;
    using System.Text.RegularExpressions;
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

            if (BasketBallResourceID.MoreResultResourceIDs.Contains(detailItem.ResourceId) ||
                BasketBallResourceID.MoreResultResourceIDRegex.Any(id => Regex.IsMatch(item.ResourceID, id)))
            {
                var betTypeId = detailItem?.ResourceId?.Substring(0, 3);
                var resourceId = detailItem?.ResourceId?.Remove(0, 3);

                item.ResourceName = SportsbookRefDataHelper.GetBetTypeNameById(betTypeId, GetSupportedLanguage, resourceId);
            }

            return MapGameValueByStatusId(item, detailItem);
        }

        protected override BasketBallGameDetailItem MapGameValueByStatusId(BasketBallGameDetailItem item, GameDetail detailItem)
        {
            var newItem = base.MapGameValueByStatusId(item, detailItem);

            newItem.HomeValue = string.IsNullOrEmpty(newItem.HomeValue) ? "-" : newItem.HomeValue;
            newItem.AwayValue = string.IsNullOrEmpty(newItem.AwayValue) ? "-" : newItem.AwayValue;

            if (detailItem.SettlementStatusID == EventStatusID.RunningSettlementStatusID)
            {
                newItem.HomeValue = ConstantValue.Hyphen;
                newItem.AwayValue = ConstantValue.Hyphen;
            }

            return newItem;
        }

        protected override void MapGameValueByPattern(BasketBallGameDetailItem item, GameDetail detailItem)
        {
            base.MapGameValueByPattern(item, detailItem);

            if (BasketBallResourceID.MoreResultResourceIDs.Contains(detailItem.ResourceId) ||
                BasketBallResourceID.MoreResultResourceIDRegex.Any(id => Regex.IsMatch(item.ResourceID, id)))
            {
                MapGameValueForMoreResult(item, detailItem);
            }
        }

        protected void MapGameValueForMoreResult(BasketBallGameDetailItem item, GameDetail detailItem)
        {
            var isHomeTeam = detailItem?.HomeValue?.ToLowerInvariant() == "h";
            var isNone = detailItem?.HomeValue?.ToLowerInvariant() == "n";

            var result = isHomeTeam ? HomeTeam : AwayTeam;

            item.IsHome = !isNone && isHomeTeam;
            item.IsAway = !isNone && !isHomeTeam;
            item.HomeValue = isNone ? LangResource.None : result;
        }
    }
}