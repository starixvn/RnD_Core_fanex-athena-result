namespace Fanex.Result.Core.Volleyball.DTO
{
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Core.Sportsbook.Resources;
    using Fanex.Result.Core.Sportsbook.Utilities;
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;

    public class MapVolleyball : BaseResultDetail<MapVolleyballResult>
    {
        public MapVolleyball(
            Sportsbook sportsbook,
            ResultSetting setting,
            IRefDataHelper refDataHelper,
            ISportsbookHelperWraper sportsBookHelper)
            : base(sportsbook, setting, refDataHelper, sportsBookHelper)
        {
        }

        protected override MapVolleyballResult CreateGameDetailItem(GameDetail detailItem)
        {
            var mapResult = base.CreateGameDetailItem(detailItem);
            const int MinLengthResourceId = 4;

            if (!string.IsNullOrWhiteSpace(detailItem.ResourceId)
                && detailItem.ResourceId.Length >= MinLengthResourceId)
            {
                var betTypeId = detailItem.ResourceId.Substring(0, MinLengthResourceId);
                var resourceId = detailItem.ResourceId.Replace(betTypeId, string.Empty);
                mapResult.BetTypeId = int.Parse(betTypeId);

                var mapResultItem = MapVolleyballResultMetadata.GetMapByBettypeId(mapResult.BetTypeId);
                if (mapResultItem != null)
                {
                    
                    mapResult.SetNumber = GetSetValue(detailItem.ResourceId);
                    mapResult.Point = GetPointValue(detailItem.ResourceId);
                    mapResult.ResourceName = GetResourceName(mapResult, betTypeId, resourceId, mapResultItem);
                    mapResult.Visible = detailItem.SettlementStatusID > EventStatusID.RunningSettlementStatusID;
                }
            }                

            return MapGameValueByStatusId(mapResult, detailItem);
        }

        private string GetResourceName(
            MapVolleyballResult mapResult,
            string betTypeId,
            string resourceId,
            MapVolleyballResultMetadata mapResultItem)
            => mapResultItem != null
                && !string.IsNullOrEmpty(mapResultItem.LocalResourceKey)
                    ? GetLocalResource(mapResultItem, mapResult.SetNumber, mapResult.Point)
                    : RefDataHelper.GetBetTypeNameById(betTypeId, GetSupportedLanguage, resourceId);

        private static int GetPointValue(string resourceId)
        {
            const int indexOfPoint = 6;
            const int lengthOfPoint = 2;
            return resourceId.Length == indexOfPoint + lengthOfPoint
                ? int.Parse(resourceId.Substring(indexOfPoint, lengthOfPoint))
                : 0;
        }

        private static int GetSetValue(string resourceId)
        {
            const int indexOfSetNumber = 4;
            const int lengthOfSetNumber = 2;
            return resourceId.Length >= indexOfSetNumber + lengthOfSetNumber
                ? int.Parse(resourceId.Substring(indexOfSetNumber, lengthOfSetNumber))
                : 0;
        }

        private static string GetLocalResource(MapVolleyballResultMetadata mapResultItem, int setNumber, int pointValue)
        {
            var localResource = SportsbookResx.ResourceManager.GetString(mapResultItem.LocalResourceKey);

            if (!string.IsNullOrWhiteSpace(localResource))
            {
                return pointValue > 0
                    ? string.Format(localResource, setNumber, pointValue)
                    : string.Format(localResource, setNumber);
            }

            return localResource;
        }

        protected override MapVolleyballResult MapGameValueByStatusId(MapVolleyballResult item, GameDetail detailItem)
        {
            var newItem = base.MapGameValueByStatusId(item, detailItem);

            newItem.HomeValue = string.IsNullOrEmpty(newItem.HomeValue) ? ConstantValue.Hyphen : newItem.HomeValue;
            newItem.AwayValue = string.IsNullOrEmpty(newItem.AwayValue) ? ConstantValue.Hyphen : newItem.AwayValue;

            if (detailItem.SettlementStatusID == EventStatusID.RunningSettlementStatusID)
            {
                newItem.HomeValue = ConstantValue.Hyphen;
                newItem.AwayValue = ConstantValue.Hyphen;
            }

            if (detailItem.SettlementStatusID == EventStatusID.RefundSettlementStatusID)
            {
                newItem.HomeValue = LangResource.Refund;
                newItem.AwayValue = LangResource.Refund;
            }

            return newItem;
        }

        protected override void MapGameValueByPattern(MapVolleyballResult item, GameDetail detailItem)
        {
            base.MapGameValueByPattern(item, detailItem);

            if (item.BetTypeId > 0
                && MapVolleyballResultMetadata.SupportBettype(item.BetTypeId))
            {
                var resultMetadata = MapVolleyballResultMetadata.GetMapByBettypeId(item.BetTypeId);

                if (resultMetadata.IsSingleResult)
                {
                    item.IsDisplaySingleTeam = resultMetadata.IsSingleResult;
                    HandleSingleResult(item, detailItem);
                }
                else
                {
                    var homeValue = detailItem?.HomeValue?.ToLowerInvariant();
                    var awayValue = detailItem?.AwayValue?.ToLowerInvariant();
                    if (homeValue == awayValue && awayValue == ConstantValue.N)
                    {
                        item.IsDisplaySingleTeam = true;
                        item.HomeValue = item.AwayValue = LangResource.None;
                    }
                    else
                    {
                        item.HomeValue = homeValue;
                        item.AwayValue = awayValue;
                    }
                }
            }
        }

        private void HandleSingleResult(MapVolleyballResult item, GameDetail detailItem)
        {
            var homeValue = detailItem?.HomeValue?.ToLowerInvariant();
            var isHomeTeam = homeValue == ConstantValue.Home;
            var isNoneOrNo = homeValue == ConstantValue.N;
            if (isNoneOrNo)
            {
                HandleNoOrNoneResult(item);
            }
            else if (homeValue == ConstantValue.Y)
            {
                item.HomeValue = LangResource.Yes;
            }
            else if (MapVolleyballResultMetadata.SupportBettype(item.BetTypeId)
                                        && MapVolleyballResultMetadata.GetMapByBettypeId(item.BetTypeId).IsSingleScore)
            {
                item.HomeValue = homeValue;
            }
            else
            {
                HandleTeamNameResult(item, isHomeTeam);
            }
        }

        private static void HandleNoOrNoneResult(MapVolleyballResult item)
        {
            item.HomeValue = MapVolleyballResultMetadata.SupportBettype(item.BetTypeId)
                                        && MapVolleyballResultMetadata.GetMapByBettypeId(item.BetTypeId).IsYesNo
                                        ? LangResource.No
                                        : LangResource.None;
        }

        private void HandleTeamNameResult(MapVolleyballResult item, bool isHomeTeam)
        {
            var result = isHomeTeam ? HomeTeam : AwayTeam;
            item.HomeValue = result;
        }
    }
}