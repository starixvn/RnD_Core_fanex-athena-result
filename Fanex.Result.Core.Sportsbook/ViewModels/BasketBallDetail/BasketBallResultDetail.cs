namespace Fanex.Result.Core.Sportsbook.ViewModels.BasketBallDetail
{
    using System.Linq;
    using Base;
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

                item.ResourceName = RefDataHelper.GetBetTypeNameById(betTypeId, GetSupportedLanguage, resourceId);
            }

            return MapGameValueByStatusId(item, detailItem);
        }

        protected override void MapGameValueByPattern(BasketBallGameDetailItem item, GameDetail detailItem)
        {
            base.MapGameValueByPattern(item, detailItem);

            if (BasketBallResourceID.MoreResultResourceIDs.Contains(detailItem.ResourceId))
            {
                var isHomeTeam = detailItem?.HomeValue?.ToLowerInvariant() == "h";
                var isNone = detailItem?.HomeValue?.ToLowerInvariant() == "n";

                var result = isHomeTeam ? HomeTeam : AwayTeam;

                item.HomeValue = isNone ? LangResource.None : result;
            }
        }
    }
}