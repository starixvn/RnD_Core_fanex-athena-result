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
    using Fanex.Result.Core.Base.Utilities;
    using Utilities;

    public class BasketBallResultDetail : BaseResultDetail<BasketBallGameDetailItem>
    {
        public BasketBallResultDetail(
            Sportsbook sportsbook,
            ResultSetting setting,
            IRefDataHelper refDataHelper,
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

                item.ResourceName = RefDataHelper.GetBetTypeNameById(betTypeId, GetSupportedLanguage, resourceId);
            }

            if (SportsbookHelper.IsBasketBallPropBettype(detailItem.ResourceId))
            {
                item.ResourceName = GetBasketBallPropsHeaderDetail(detailItem.ResourceId);
                item.Players =  new BasketballPlayerInfos(detailItem.OtherInfo);
                item.HomeValue = GetPropResultValue(detailItem);
                item.SettlementStatusID = detailItem.SettlementStatusID;
            }
            

            return MapGameValueByStatusId(item, detailItem);
        }

        protected override BasketBallGameDetailItem MapGameValueByStatusId(BasketBallGameDetailItem item, GameDetail detailItem)
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

        private string GetBasketBallPropsHeaderDetail(string resourceId)
        {
            if (string.IsNullOrWhiteSpace(resourceId))
            {
                return string.Empty;
            }
            
            foreach (var kvp in BasketBallResourceID.PlayerPropsHeaderResourceIDs)
            {
                if (Regex.IsMatch(resourceId, kvp.Key, RegexOptions.Compiled))
                {
                    return kvp.Value;
                }
            }

            return string.Empty;
        }
        
        private static string GetPropResultValue(GameDetail item)
        {
            return item.SettlementStatusID switch
            {
                EventStatusID.RefundSettlementStatusID => LangResource.Refund,
                EventStatusID.RefundWithSettleDetermined => LangResource.RefundWithSettleDetermined,
                EventStatusID.RunningSettlementStatusID => ConstantValue.Hyphen,
                _ => item.HomeValue,
            };
        }
    }
}