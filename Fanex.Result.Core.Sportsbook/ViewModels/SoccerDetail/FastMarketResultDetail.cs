namespace Fanex.Result.Core.Sportsbook.ViewModels.SoccerDetail
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Core.Sportsbook.Utilities;
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;

    public class FastMarketResultDetail : BaseResultDetail<SoccerGameDetailItem>
    {
        private const string SituationDetailResourceId = "221CNT0";
        private const string FastMarketRefId = "FastMarket";

        public FastMarketResultDetail(
            Sportsbook sportsbook,
            ResultSetting setting,
            ISportsbookRefDataHelper refDataHelper = null,
            ISportsbookHelperWraper sportsBookHelper = null)
            : base(sportsbook, setting, refDataHelper, sportsBookHelper)
        {
        }

        public override SafeList<SoccerGameDetailItem> GetDetailItems()
        {
            var detailItems = Sportsbook.GameDetails?
                .Where(i => SportsbookHelper.IsFastMarketsResourceId(i.ResourceId))
                .Select(item => CreateGameDetailItem(item));

            return detailItems.ToSafeList();
        }

        protected override SoccerGameDetailItem CreateGameDetailItem(GameDetail detailItem)
        {
            var item = new SoccerGameDetailItem
            {
                GroupCode = detailItem.ResourceId,
                ResourceID = detailItem.ResourceId,
                GroupName = SportsbookRefDataHelper.GetBetTypeNameById(Sportsbook.Bettype.ToString(), GetSupportedLanguage),
                TimeRange = new List<string> { SportsbookHelper.CreateFastMarketsTimeRange(detailItem.ResourceId, Sportsbook.BetID) }
            };

            return MapGameValueByStatusId(item, detailItem);
        }

        protected override void MapGameValueByPattern(SoccerGameDetailItem item, GameDetail detailItem)
        {
            bool isFastMarketTwo = Sportsbook.Bettype != BetTypeID.NextOneMinuteSetPiece &&
                Sportsbook.GameDetails.Any(gameDetail => gameDetail.ResourceId.StartsWith(SituationDetailResourceId));
            var startTime = Convert.ToInt32(Sportsbook.BetID);
            var situationId = Sportsbook.Handicap2;

            switch (item.ResourceID)
            {
                case GameResourceID.NextOneMinutes:
                case GameResourceID.NextFiveMinutes:
                    if (isFastMarketTwo)
                    {
                        GenerateDetailForFastMarketTwo(item, startTime, situationId);
                    }
                    else
                    {
                        item.SituationValue = new List<string> { GetFastMarketSituation(detailItem, startTime, Sportsbook.Bettype) };
                    }

                    break;

                case GameResourceID.FirstOneMinutes:
                case GameResourceID.FirstFiveMinutes:
                case GameResourceID.NextOneMinuteSetPiece:
                case GameResourceID.WhichCombinationFirstOneMinutes:
                case GameResourceID.WhichCombinationFirstFiveMinutes:
                    item.SituationValue = new List<string> { GetFastMarketSituation(detailItem, startTime, Sportsbook.Bettype) };
                    break;

                default:
                    base.MapGameValueByPattern(item, detailItem);
                    break;
            }
        }

        private void GenerateDetailForFastMarketTwo(SoccerGameDetailItem item, int startTime, int situationId)
        {
            var situationName = GetSituationName(situationId);

            if (item.ResourceID == GameResourceID.NextFiveMinutes)
            {
                item.TimeRange = new List<string>();
                item.SituationValue = new List<string>();

                for (int i = startTime; i < startTime + 5; i++)
                {
                    item.TimeRange.Add(SportsbookHelper.CreateFastMarketsTimeRange(GameResourceID.NextOneMinutes, i.ToString()));
                    item.SituationValue.Add($"{situationName}: {GetSituationHappenedTimes(i, situationId)}");
                }
            }
            else
            {
                item.SituationValue = new List<string> { $"{situationName}: {GetSituationHappenedTimes(startTime, situationId)}" };
            }
        }

        private string GetSituationName(int situationId)
            => SportsbookRefDataHelper.GetResourceDataByRefIdAndResourceId(FastMarketRefId, situationId.ToString(), GetSupportedLanguage, string.Empty);

        private string GetSituationHappenedTimes(int startTime, int situationId)
        {
            var situationResultResourceId = SituationDetailResourceId + (Math.Log(situationId, 2) + 1);

            var situationRecord = Sportsbook
                .GameDetails
                .FirstOrDefault(detail => detail.ResourceId.Contains(situationResultResourceId));

            var situationHappenedTimes = startTime < 45 ? situationRecord?.HomeValue[startTime] : situationRecord?.AwayValue[startTime % 45];

            return situationHappenedTimes.ToString();
        }

        private string GetFastMarketSituation(GameDetail detailedItem, int startTime, int betType)
        {
            var results = startTime < 45
                ? Encoding.Default.GetBytes(detailedItem.HomeValue)
                : Encoding.Default.GetBytes(detailedItem.AwayValue);

            var result = results[startTime % 45];
            var situation = betType == 225 ?
                SportsbookRefDataHelper.GetFastMarket225Result(result.ToString(), GetSupportedLanguage) :
                SportsbookRefDataHelper.GetFastMarketResult(result.ToString(), GetSupportedLanguage);

            return situation;
        }
    }
}