namespace Fanex.Result.Core.Sportsbook.ViewModels.VSBasketBallDetail
{
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Core.Sportsbook.Utilities;
    using Fanex.Result.Core.Sportsbook.ViewModels.BasketBallDetail;

    public class VSBasketBallResultDetail : BasketBallResultDetail
    {
        public VSBasketBallResultDetail(
            Sportsbook sportsbook,
            ResultSetting setting,
            ISportsbookRefDataHelper refDataHelper = null,
            ISportsbookHelperWraper sportsBookHelper = null)
            : base(sportsbook, setting, refDataHelper, sportsBookHelper)
        {
        }

        protected override BasketBallGameDetailItem CreateGameDetailItem(GameDetail detailItem)
        {
            var item = base.CreateGameDetailItem(detailItem);

            if (VSBasketBallResourceID.MoreResultResourceIDs.Contains(detailItem.ResourceId))
            {
                var betTypeId = detailItem?.ResourceId?.Substring(0, 4);
                var resourceId = detailItem?.ResourceId?.Remove(0, 4);

                item.ResourceName = SportsbookRefDataHelper.GetBetTypeNameById(betTypeId, GetSupportedLanguage, resourceId);
            }

            return MapGameValueByStatusId(item, detailItem);
        }

        protected override void MapGameValueByPattern(BasketBallGameDetailItem item, GameDetail detailItem)
        {
            base.MapGameValueByPattern(item, detailItem);

            if (VSBasketBallResourceID.MoreResultResourceIDs.Contains(detailItem.ResourceId))
            {
                MapGameValueForMoreResult(item, detailItem);
            }
        }
    }
}