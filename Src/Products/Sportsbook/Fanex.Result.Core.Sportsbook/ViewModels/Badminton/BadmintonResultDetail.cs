namespace Fanex.Result.Core.Sportsbook.ViewModels.Badminton
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using Base;
    using BaseDetail;
    using DTO;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Utilities;

    public class BadmintonResultDetail : BaseResultDetail<BadmintonlGameDetailItem>
    {
        public BadmintonResultDetail(
            Sportsbook sportsbook,
            ResultSetting setting,
            IRefDataHelper refDataHelper,
            ISportsbookHelperWraper sportsBookHelper)
            : base(sportsbook, setting, refDataHelper, sportsBookHelper)
        {
        }

        protected override BadmintonlGameDetailItem CreateGameDetailItem(GameDetail detailItem)
        {
            var item = base.CreateGameDetailItem(detailItem);

            if (BadmintonResourceID.MoreResultResourceIDRegex.Any(id => Regex.IsMatch(item.ResourceID, id)))
            {
                var betTypeId = detailItem?.ResourceId?.Substring(0, 3);
                var resourceId = detailItem?.ResourceId?.Remove(0, 3);
                var gameNumber = detailItem?.ResourceId?.Substring(3, 2);

                item.ResourceName = RefDataHelper.GetBetTypeNameById(betTypeId, GetSupportedLanguage, resourceId);
                item.GameName = LangResource.ResourceManager.GetString($"Game{gameNumber}");
                item.GameNumber = gameNumber;
            }

            return MapGameValueByStatusId(item, detailItem);
        }

        protected override void MapGameValueByPattern(BadmintonlGameDetailItem item, GameDetail detailItem)
        {
            base.MapGameValueByPattern(item, detailItem);

            if (BadmintonResourceID.MoreResultResourceIDRegex.Any(id => Regex.IsMatch(item.ResourceID, id)))
            {
                MapGameValueForMoreResult(item, detailItem);
            }
        }

        protected override BadmintonlGameDetailItem MapGameValueByStatusId(BadmintonlGameDetailItem item, GameDetail detailItem)
        {
            var newItem = base.MapGameValueByStatusId(item, detailItem);
            if(detailItem.SettlementStatusID == EventStatusID.RunningSettlementStatusID)
            {
                newItem.HomeValue = ConstantValue.Hyphen;
                newItem.AwayValue = ConstantValue.Hyphen;
            }

            return newItem;
        }
        protected void MapGameValueForMoreResult(BadmintonlGameDetailItem item, GameDetail detailItem)
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