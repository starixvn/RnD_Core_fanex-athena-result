namespace Fanex.Result.Core.ESport.DTO
{
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Core.Sportsbook.Utilities;
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Extension;
    using System.Linq;

    public class Map : BaseResultDetail<MapResult>
    {
        public Map(
            Sportsbook sportsbook,
            ResultSetting setting,
            IRefDataHelper refDataHelper,
            ISportsbookHelperWraper sportsBookHelper)
            : base(sportsbook, setting, refDataHelper, sportsBookHelper)
        {
        }

        protected override MapResult CreateGameDetailItem(GameDetail detailItem)
        {
            var mapResult = base.CreateGameDetailItem(detailItem);

            if (!string.IsNullOrWhiteSpace(detailItem.ResourceId)
                && detailItem.ResourceId.Length > 4)
            {
                var betTypeId = detailItem.ResourceId.Substring(0, 4);
                var resourceId = detailItem.ResourceId.Replace(betTypeId, string.Empty);

                mapResult.MapId = int.Parse(detailItem.ResourceId.Substring(4, 2));
                mapResult.ResourceName = RefDataHelper.GetBetTypeNameById(betTypeId, GetSupportedLanguage, resourceId);
                mapResult.BetTypeId = int.Parse(betTypeId);
            }

            return MapGameValueByStatusId(mapResult, detailItem);
        }

        protected override MapResult MapGameValueByStatusId(MapResult item, GameDetail detailItem)
        {
            var newItem = base.MapGameValueByStatusId(item, detailItem);

            newItem.HomeValue = string.IsNullOrEmpty(newItem.HomeValue) ? ConstantValue.Hyphen : newItem.HomeValue;
            newItem.AwayValue = string.IsNullOrEmpty(newItem.AwayValue) ? ConstantValue.Hyphen : newItem.AwayValue;

            if (detailItem.SettlementStatusID == EventStatusID.RunningSettlementStatusID)
            {
                newItem.HomeValue = ConstantValue.Hyphen;
                newItem.AwayValue = ConstantValue.Hyphen;
            }

            if(detailItem.SettlementStatusID == EventStatusID.RefundSettlementStatusID)
            {
                newItem.HomeValue = ConstantValue.R;
                newItem.AwayValue = ConstantValue.R;
            }

            return newItem;
        }

        protected override void MapGameValueByPattern(MapResult item, GameDetail detailItem)
        {
            base.MapGameValueByPattern(item, detailItem);

            if (item.BetTypeId > 0 
                && MapResultMetadata.ResultTypeOrder.Keys.Contains(item.BetTypeId))
            {
                var resultMetadata = MapResultMetadata.ResultTypeOrder[item.BetTypeId];

                if (resultMetadata.IsSingleResult)
                {
                    var homeValue = detailItem?.HomeValue?.ToLowerInvariant();
                    var isHomeTeam = homeValue == "h";
                    var isNoneOrNo = homeValue == "n";
                    var isDraw = homeValue == "d";
                    var isYes = homeValue == "y";
                    item.IsDisplaySingleTeam = true;

                    if (isNoneOrNo)
                    {
                        item.HomeValue = MapResultMetadata.ResultTypeOrder.Keys.Contains(item.BetTypeId)
                            && MapResultMetadata.ResultTypeOrder[item.BetTypeId].IsYesNo
                            ? LangResource.No
                            : LangResource.None;
                    }
                    else if (isDraw)
                    {
                        item.HomeValue = LangResource.Draw;
                    }
                    else if (isYes)
                    {
                        item.HomeValue = LangResource.Yes;
                    }
                    else
                    {
                        if (MapResultMetadata.ResultTypeOrder.Keys.Contains(item.BetTypeId)
                            && MapResultMetadata.ResultTypeOrder[item.BetTypeId].IsDuration)
                        {
                            item.HomeValue = string.Format(LangResource.MinSec, item.HomeValue, item.AwayValue);
                        }
                        else
                        {
                            var result = isHomeTeam ? HomeTeam : AwayTeam;
                            item.IsDisplaySingleTeam = !isNoneOrNo && isHomeTeam;
                            item.HomeValue = result;
                        }
                    }
                }
                else
                {
                    item.IsDisplayBothTeam = true;
                    item.HomeValue = detailItem.HomeValue;
                    item.AwayValue = detailItem.AwayValue;
                }
            }
        }
    }
}