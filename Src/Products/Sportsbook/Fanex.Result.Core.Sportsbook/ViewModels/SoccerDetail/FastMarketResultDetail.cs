namespace Fanex.Result.Core.Sportsbook.ViewModels.SoccerDetail
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Core.Sportsbook.Resources;
    using Fanex.Result.Core.Sportsbook.Utilities;
    using Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail;

    public class FastMarketResultDetail : BaseResultDetail<SoccerGameDetailItem>
    {
        private const int TimeOfOneHalf = 45;
        private const int InjuryTime = 46;
        private const string SituationDetailResourceId = "221CNT0";
        private const string FastMarketRefId = "FastMarket";

        public FastMarketResultDetail(
            Sportsbook sportsbook,
            ResultSetting setting,
            IRefDataHelper refDataHelper = null,
            ISportsbookHelperWraper sportsBookHelper = null)
            : base(sportsbook, setting, refDataHelper, sportsBookHelper)
        {
        }

        public override SafeList<SoccerGameDetailItem> GetDetailItems()
        {
            var detailItems = Sportsbook.GameDetails?
                .Where(i => SportsbookHelper.IsFastMarketsResourceId(i.ResourceId))
                .Select(item => CreateGameDetailItem(item));

            var overUnderHasResultDetail = detailItems.Where(i => i.ResourceID == GameResourceID.TimeMachineOverUnderStatus && i.SituationValue.Any());
            var othersBetType = detailItems.Where(i => i.ResourceID != GameResourceID.TimeMachineOverUnderStatus);            

            return othersBetType.Concat(overUnderHasResultDetail).ToSafeList();
        }

        protected override SoccerGameDetailItem CreateGameDetailItem(GameDetail detailItem)
        {
            var item = new SoccerGameDetailItem
            {
                GroupCode = detailItem.ResourceId,
                ResourceID = detailItem.ResourceId,
                GroupName = RefDataHelper.GetBetTypeNameById(Sportsbook.Bettype.ToString(), GetSupportedLanguage),
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

                case GameResourceID.TimeMachineOverUnderStatus:
                    var result = GetTotalGoalOfTimeMachineOU(startTime, GameResourceID.TimeMachineOverUnderGoal);
                    item.SituationValue = new List<string>();
                    if (!string.IsNullOrEmpty(result))
                    {
                        item.SituationValue.Add(result);
                    }

                    break;

                case GameResourceID.TimeMachineHDPStatus:
                    item.HomeValue = GetTotalGoalOfTimeMachineHDP(startTime, isHomeTeam: true);
                    item.AwayValue = GetTotalGoalOfTimeMachineHDP(startTime, isHomeTeam: false);
                    item.SituationValue = new List<string> { item.HomeValue, item.AwayValue };
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
            => RefDataHelper.GetResourceDataByRefIdAndResourceId(FastMarketRefId, situationId.ToString(), GetSupportedLanguage, string.Empty);

        private string GetSituationHappenedTimes(int startTime, int situationId)
        {
            var situationResultResourceId = SituationDetailResourceId + (Math.Log(situationId, 2) + 1);

            var situationRecord = Sportsbook
                .GameDetails
                .FirstOrDefault(detail => detail.ResourceId.Contains(situationResultResourceId));

            var situationHappenedTimes = startTime < TimeOfOneHalf ? situationRecord?.HomeValue[startTime] : situationRecord?.AwayValue[startTime % 45];

            return situationHappenedTimes.ToString();
        }

        private string GetFastMarketSituation(GameDetail detailedItem, int startTime, int betType)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var encoding = Encoding.GetEncoding("windows-1252");
            var results = startTime < TimeOfOneHalf
                ? encoding.GetBytes(detailedItem.HomeValue)
                : encoding.GetBytes(detailedItem.AwayValue);

            var result = results[startTime % TimeOfOneHalf];
            var situation = betType == 225 ?
                RefDataHelper.GetFastMarket225Result(result.ToString(), GetSupportedLanguage) :
                RefDataHelper.GetFastMarketResult(result.ToString(), GetSupportedLanguage);

            return situation;
        }

        private string GetTotalGoalOfTimeMachineHDP(int minute, bool isHomeTeam)
        {
            var resourceId = isHomeTeam ? GameResourceID.TimeMachineHDPGoalForHome : GameResourceID.TimeMachineHDPGoalForAway;

            return GetTotalGoalOfTimeMachine(minute, resourceId);
        }

        private string GetTotalGoalOfTimeMachineOU(int minute, string resourceId)
        {
            var result = GetTotalGoalOfTimeMachine(minute, resourceId);

            return string.IsNullOrEmpty(result) ? string.Empty : $"{SportsbookResx.TotalGoal}: {result}";
        }

        private string GetTotalGoalOfTimeMachine(int minute, string resourceId)
        {
            if (IsTimeMachineCalculated(minute))
            {
                var timeMachineGoal = Sportsbook.GameDetails.First(i => i.ResourceId == resourceId);
                var totalGoals = minute < TimeOfOneHalf ?
                    TotalGoalsOfFirsHalf(timeMachineGoal, minute) :
                    TotalGoalsFullTime(timeMachineGoal, minute);

                return totalGoals.ToString();
            }

            return string.Empty;
        }

        private static int TotalGoalsOfUntilMinute(string goals, int minute)
        {
            var totalGoals = 0;
            var minIndex = minute < goals.Length + 1 ? minute : goals.Length;
            for (var i = 0; i <= minIndex; i++)
            {
                totalGoals += Convert.ToInt32(goals[i].ToString());
            }

            return totalGoals;
        }

        private static int TotalGoalsOfFirsHalf(GameDetail gameDetail, int minute)
            => TotalGoalsOfUntilMinute(gameDetail.HomeValue, minute);

        private static int TotalGoalsFullTime(GameDetail gameDetail, int minute)
        {
            var totalGoalsInjuryTime = Convert.ToInt32(gameDetail.HomeValue[InjuryTime - 1].ToString());

            return TotalGoalsOfUntilMinute(gameDetail.HomeValue, TimeOfOneHalf - 1) +
                totalGoalsInjuryTime +
                TotalGoalsOfUntilMinute(gameDetail.AwayValue, minute - TimeOfOneHalf);
        }

        private int GetTimeMachineStatus(int minute)
        {
            var timeMachineStatusResourceId = new List<string>
            {
                GameResourceID.TimeMachineOverUnderStatus,
                GameResourceID.TimeMachineHDPStatus
            };
            var timeMachineStatus = Sportsbook.GameDetails.First(i => timeMachineStatusResourceId.Contains(i.ResourceId));
            var statusString = timeMachineStatus.HomeValue.Substring(0, TimeOfOneHalf);
            if (minute >= TimeOfOneHalf)
            {
                statusString = timeMachineStatus.AwayValue.Substring(0, TimeOfOneHalf);
            }

            return Convert.ToInt32(statusString[minute % TimeOfOneHalf].ToString());
        }

        private bool IsTimeMachineCalculated(int minute)
        {
            const int calculatedAutomaticallyStatus = 4;

            var status = GetTimeMachineStatus(minute);

            return status >= calculatedAutomaticallyStatus;
        }
    }
}