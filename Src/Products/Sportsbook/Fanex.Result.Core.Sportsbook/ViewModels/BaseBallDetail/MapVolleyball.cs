namespace Fanex.Result.Core.Volleyball.DTO
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Baseball.DTO;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Core.Sportsbook.Resources;
    using Fanex.Result.Core.Sportsbook.Utilities;
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;

    public class MapBaseball : BaseResultDetail<MapBaseballResult>
    {
        public MapBaseball(
            Sportsbook sportsbook,
            ResultSetting setting,
            IRefDataHelper refDataHelper,
            ISportsbookHelperWraper sportsBookHelper)
            : base(sportsbook, setting, refDataHelper, sportsBookHelper)
        {
        }

        protected override MapBaseballResult CreateGameDetailItem(GameDetail detailItem)
        {
            var mapResult = base.CreateGameDetailItem(detailItem);
            const int MinLengthResourceId = 4;

            if (!string.IsNullOrWhiteSpace(detailItem.ResourceId)
                && detailItem.ResourceId.Length >= MinLengthResourceId)
            {
                var betTypeId = detailItem.ResourceId.Substring(0, MinLengthResourceId);
                var resourceId = detailItem.ResourceId.Replace(betTypeId, string.Empty);
                mapResult.BetTypeId = int.Parse(betTypeId);

                var mapResultItem = MapBaseballResultMetadata.GetMapByBettypeId(mapResult.BetTypeId);
                if (mapResultItem != null)
                {
                    mapResult.IsDisplaySingleTeam = mapResultItem.IsSingleResult;
                    mapResult.ResourceName = string.IsNullOrEmpty(mapResultItem.LocalResourceKey) ?
                                                 RefDataHelper.GetDisplayedBetTypeName(betTypeId, GetSupportedLanguage, resourceId)
                                                 : GetLocalResource(mapResultItem.LocalResourceKey, resourceId);
                }
            }

            return MapGameValueByStatusId(mapResult, detailItem);
        }

        protected override MapBaseballResult MapGameValueByStatusId(MapBaseballResult item, GameDetail detailItem)
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

        protected override void MapGameValueByPattern(MapBaseballResult item, GameDetail detailItem)
        {
            base.MapGameValueByPattern(item, detailItem);

            if (item.BetTypeId > 0
                && MapBaseballResultMetadata.SupportBettype(item.BetTypeId))
            {
                var resultMetadata = MapBaseballResultMetadata.GetMapByBettypeId(item.BetTypeId);

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

        private void HandleSingleResult(MapBaseballResult item, GameDetail detailItem)
        {
            var homeValue = detailItem?.HomeValue?.ToLowerInvariant();
            var isHomeTeam = homeValue == ConstantValue.Home;
            var isNoneOrNo = homeValue == ConstantValue.N;
            if (isNoneOrNo)
            {
                HandleNoOrNeigherResult(item);
            }
            else if (homeValue == ConstantValue.Y)
            {
                item.HomeValue = LangResource.Yes;
            }
            else
            {
                HandleTeamNameResult(item, isHomeTeam);
            }
        }

        private static void HandleNoOrNeigherResult(MapBaseballResult item)
        => item.HomeValue = MapBaseballResultMetadata.GetMapByBettypeId(item.BetTypeId).IsYesNo
                                        ? LangResource.No
                                        : LangResource.Neither;

        private void HandleTeamNameResult(MapBaseballResult item, bool isHomeTeam)
        {
            var result = isHomeTeam ? HomeTeam : AwayTeam;
            item.HomeValue = result;
        }

        private string GetLocalResource(string localResourceKey, string resourceId)
        {
            var localResource = SportsbookResx.ResourceManager.GetString(localResourceKey);
            var replaceText = GetNoTextForEnglish(resourceId);

            return localResource.Replace("X ", $"{replaceText} ");
        }

        private string GetNoTextForEnglish(string resourceId)
        {
            var replacedText = new List<string> { "X", "First", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th" };
            var isNumber = int.TryParse(resourceId, out int resId);
            if (isNumber)
            {
                resourceId = GetSupportedLanguage == "en-US" && resId < replacedText.Count()
                    ? replacedText[resId]
                    : resId.ToString();
            }

            return resourceId;
        }
    }
}