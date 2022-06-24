namespace Fanex.Result.Core.ESport.DTO
{
    using System;
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Core.Sportsbook.Resources;
    using Fanex.Result.Core.Sportsbook.Utilities;
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;

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

                mapResult.BetTypeId = int.Parse(betTypeId);

                var mapResultItem = MapResultMetadata.ResultTypeOrder.Keys.Contains(mapResult.BetTypeId)
                                    ? MapResultMetadata.ResultTypeOrder[mapResult.BetTypeId]
                                    : null;

                mapResult.MapId = int.Parse(detailItem.ResourceId.Substring(4, 2));
                mapResult.RoundId = GetRoundId(detailItem.ResourceId);
                mapResult.ResourceName = GetResourceName(mapResult, betTypeId, resourceId, mapResultItem);
                mapResult.Visible = detailItem.SettlementStatusID > 0;
            }

            if(!string.IsNullOrWhiteSpace(detailItem.ResourceId) 
                && detailItem.ResourceId.StartsWith("M", StringComparison.CurrentCultureIgnoreCase))
            {
                const int mainBettype = 9001;
                var mapResultItem = MapResultMetadata.ResultTypeOrder[mainBettype];
                mapResult.BetTypeId = mainBettype;
                mapResult.MapId = int.Parse(detailItem.ResourceId.Substring(1));
                mapResult.ResourceName = string.Format(SportsbookResx.ResourceManager.GetString(mapResultItem.LocalResourceKey), mapResult.MapId);
                mapResult.Visible = detailItem.SettlementStatusID > 0;
            }

            return MapGameValueByStatusId(mapResult, detailItem);
        }

        private string GetResourceName(
            MapResult mapResult,
            string betTypeId,
            string resourceId,
            MapResultMetadata mapResultItem)
            => mapResultItem != null
                && !string.IsNullOrEmpty(mapResultItem.LocalResourceKey)
                    ? GetLocalResource(mapResultItem, mapResult.MapId, mapResult.RoundId)
                    : RefDataHelper.GetBetTypeNameById(betTypeId, GetSupportedLanguage, resourceId);

        private static int GetRoundId(string resourceId)
            => !string.IsNullOrWhiteSpace(resourceId) && resourceId.Length >= 8
                ? int.Parse(resourceId.Substring(6, 2))
                : 0;

        private static string GetLocalResource(MapResultMetadata mapResultItem, int mapId, int roundId)
        {
            var localResource = SportsbookResx.ResourceManager.GetString(mapResultItem.LocalResourceKey);

            if (!string.IsNullOrWhiteSpace(localResource))
            {
                if (mapResultItem.MapXOrdinalY)
                {
                    var ordinalList = new Dictionary<int, string>
                    {
                        { 1, SportsbookResx.FirstNumber},
                        { 2, SportsbookResx.SecondNumber},
                        { 3, SportsbookResx.ThirdNumber},
                        { 4, SportsbookResx.FourthNumber},
                        { 5, SportsbookResx.FifthNumber},
                        { 6, SportsbookResx.SixthNumber},
                        { 7, SportsbookResx.SeventhNumber},
                        { 8, SportsbookResx.EighthNumber},
                        { 9, SportsbookResx.NinthNumber},
                        { 10, SportsbookResx.TenthNumber},
                        { 15, SportsbookResx.fifteenthNumber},
                        { 20, SportsbookResx.TwentiethNumber},
                    };
                    var ordinalNumber = ordinalList.ContainsKey(roundId) ? ordinalList[roundId] : roundId.ToString();

                    return string.Format(localResource, mapId, ordinalNumber);
                }

                return roundId > 0
                    ? string.Format(localResource, mapId, roundId)
                    : string.Format(localResource, mapId);
            }

            return localResource;
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

            if (detailItem.SettlementStatusID == EventStatusID.RefundSettlementStatusID)
            {
                var refundText = item.MapId == 0 && detailItem.ResourceId != ESportResourceID.FinalScore
                    ? ConstantValue.R
                    : LangResource.Refund;
                newItem.HomeValue = refundText;
                newItem.AwayValue = refundText;
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
                    HandleSingleResult(item, detailItem);
                }
                else
                {
                    item.IsDisplayBothTeam = true;
                    item.HomeValue = detailItem.HomeValue;
                    item.AwayValue = detailItem.AwayValue;
                }
            }
        }

        private void HandleSingleResult(MapResult item, GameDetail detailItem)
        {
            var homeValue = detailItem?.HomeValue?.ToLowerInvariant();
            var result = new Dictionary<string, string>
            {
                { "d", LangResource.Draw},
                { "y", LangResource.Yes},
                { "m", SportsbookResx.Mid},
                { "nm", SportsbookResx.TopBottom},
                { "t", SportsbookResx.Top},
                { "b", SportsbookResx.Bottom},
                { "nb", SportsbookResx.TopMid},
                { "nt", SportsbookResx.MidBottom},
                { "o", SportsbookResx.Overlord},
                { "none", LangResource.None},
            };
            var isHomeTeam = homeValue == "h";
            var isNoneOrNo = homeValue == "n";
            item.IsDisplaySingleTeam = true;

            if (isNoneOrNo)
            {
                HandleNoOrNoneResult(item);
            }
            else if (result.ContainsKey(homeValue))
            {
                var resultOverlordTyrant = new Dictionary<string, string>
                {
                    { "o", SportsbookResx.Overlord},
                    { "t", SportsbookResx.Tyrant},
                    { "nt", SportsbookResx.OverlordMiddle},
                };
                item.HomeValue = MapResultMetadata.ResultTypeOrder.Keys.Contains(item.BetTypeId)
                                        && MapResultMetadata.ResultTypeOrder[item.BetTypeId].IsOverlordTyrant ?
                                        resultOverlordTyrant[homeValue] : 
                                        result[homeValue];
            }
            else if ( MapResultMetadata.ResultTypeOrder.Keys.Contains(item.BetTypeId)
                                        && MapResultMetadata.ResultTypeOrder[item.BetTypeId].IsSingleScore)
            {
                item.HomeValue = homeValue;
            }
            else
            {
                HandleDurationOrTeamNameResult(item, isHomeTeam, isNoneOrNo);
            }
        }

        private static void HandleNoOrNoneResult(MapResult item)
        {
            item.HomeValue = MapResultMetadata.ResultTypeOrder.Keys.Contains(item.BetTypeId)
                                        && MapResultMetadata.ResultTypeOrder[item.BetTypeId].IsYesNo
                                        ? LangResource.No
                                        : LangResource.None;
        }

        private void HandleDurationOrTeamNameResult(MapResult item, bool isHomeTeam, bool isNoneOrNo)
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
}