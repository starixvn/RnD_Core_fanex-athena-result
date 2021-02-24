namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Base;
    using Base.DataDomain;
    using Base.Extension;
    using Base.Resources;
    using Base.ViewModels;
    using BaseDetail;
    using DataDomain;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using Models;
    using RazorEngine;
    using RazorEngine.Templating;
    using SoccerDetail;
    using Utilities;

    public sealed class CricketViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private const string TemplateNameSpace = "CricketDetail.";

        private IList<SoccerGameDetailItem> _detailedItemModels;
        private readonly IResultDetail<SoccerGameDetailItem> _resultDetail;

        public CricketViewModel(Sportsbook cricket, ResultSetting setting)
            : this(cricket, setting, null, null)
        {
        }

        public CricketViewModel(
            Sportsbook cricket,
            ResultSetting setting,
            IResultDetail<SoccerGameDetailItem> resultDetail,
            IRefDataHelper refDataHelper)
            : base(cricket, setting, refDataHelper)
        {
            _resultDetail = resultDetail ?? new SoccerResultDetail(cricket, setting);
            _detailedItemModels = new SafeList<SoccerGameDetailItem>();
        }

        public int HomeId => SPORTBOOK.HomeId;

        public int AwayId => SPORTBOOK.AwayId;

        public override bool VisibleDetailedLink
            => base.VisibleDetailedLink && HasValidGameData;

        public override bool VisibleGameDetail => HasValidGameData;

        public string HomeFirstRuns => GetRuns(CricketResourceID.HomeFirstScore, isAwayTeam: false, isFinalScore: false);

        public string HomeSecondRuns => GetRuns(CricketResourceID.HomeSecondScore, isAwayTeam: false, isFinalScore: false);

        public string AwayFirstRuns => GetRuns(CricketResourceID.AwayFirstScore, isAwayTeam: true, isFinalScore: false);

        public string AwaySecondRuns => GetRuns(CricketResourceID.AwaySecondScore, isAwayTeam: true, isFinalScore: false);

        public string HomeFinalRuns => GetRuns(CricketResourceID.FinalScore, isAwayTeam: false, isFinalScore: true);

        public string AwayFinalRuns => GetRuns(CricketResourceID.FinalScore, isAwayTeam: true, isFinalScore: true);

        public string DetailedResult
        {
            get
            {
                GetDetailedResult();

                var temp = new StringBuilder();
                foreach (var item in _detailedItemModels)
                {
                    var templateName = TemplateNameSpace + item.GroupCode;

                    var templateByGroup = Template.Instance.GetRazorTemplate(templateName);

                    if (!string.IsNullOrEmpty(templateByGroup))
                    {
                        var binderModel = new SoccerDetailViewModel
                        {
                            Name = item.GroupName,
                            DetailItems = new SafeList<SoccerGameDetailItem> { item }
                        };
                        var result = new StringBuilder();

                        using (var writer = new StringWriter(result))
                        {
                            Engine.Razor.RunCompile(templateByGroup, templateName, writer, typeof(SoccerDetailViewModel), binderModel);
                        }

                        temp.Append(result.ToString());
                    }
                }

                return temp.ToString();
            }
        }

        #region Utilities

        private bool HasValidGameData
            => SPORTBOOK.GameDetails.Any(i => !string.IsNullOrEmpty(i.ResourceId) && IsValidResourceId(i.ResourceId));

        private string GetRuns(string resourceId, bool isAwayTeam, bool isFinalScore)
        {
            if (EventStatus.Equals(LangResource.Refund, StringComparison.OrdinalIgnoreCase))
            {
                return LangResource.Refund;
            }

            var score = SPORTBOOK.GameDetails.FirstOrDefault(i => SportsbookHelper.IsValidResourceId(resourceId, i.ResourceId));
            if (score != null)
            {
                return isFinalScore && isAwayTeam ? score.AwayValue : score.HomeValue;
            }

            if (isFinalScore)
            {
                return isAwayTeam ? GetScore(SPORTBOOK.FinalAwayScore) : GetScore(SPORTBOOK.FinalHomeScore);
            }

            return ConstantValue.Hyphen;
        }

        private static bool IsValidResourceId(string resourceId)
        {
            return CricketResourceID.MoreResultGroup.Values.Any(pattern => IsValidResourceIdByPattern(pattern, resourceId));

            bool IsValidResourceIdByPattern(List<string> patterns, string id)
                => patterns.Any(i => SportsbookHelper.IsValidResourceId(i, id));
        }

        private void GetDetailedResult()
        {
            foreach (var group in CricketResourceID.MoreResultGroup)
            {
                foreach (var game in group.Value)
                {
                    GenerateGameDetailItems(group, game);
                }
            }

            _detailedItemModels = _detailedItemModels.OrderBy(item => item.ResourceID).ToList();
        }

        private void GenerateGameDetailItems(KeyValuePair<string, List<string>> group, string game)
        {
            var detailedGameRecords = SPORTBOOK.GameDetails
                .Where(i => SportsbookHelper.IsValidResourceId(game, i.ResourceId)).ToSafeList();

            foreach (GameDetail detailedItem in detailedGameRecords)
            {
                var detailedItemModel = MapDetailedGameValue(group.Key, detailedItem);
                _detailedItemModels.Add(detailedItemModel);
            }
        }

        private SoccerGameDetailItem MapDetailedGameValue(string groupCode, GameDetail detailedItem)
        {
            var item = new SoccerGameDetailItem
            {
                GroupCode = groupCode,
                ResourceID = detailedItem.ResourceId,
                ResourceName = GetResultName(detailedItem.BettypeId, Setting.LanguageCode, detailedItem.ResourceId),
                SettlementStatusID = detailedItem.SettlementStatusID
            };

            return MapMoreDetailedGameValue(item, detailedItem);

            string GetResultName(int betType, string languageCode, string resourceId)
            {
                const int minLength = 8;
                const int firstIndexResourceId = 5;
                var id = BetTypeID.CricketBettypeGroup.ContainsKey(betType) ? BetTypeID.CricketBettypeGroup[betType].ToString() : resourceId;
                if (resourceId.Length >= minLength)
                {
                    resourceId = resourceId.Substring(firstIndexResourceId);
                }

                return RefDataHelper.GetBetTypeNameById(id, languageCode, resourceId);
            }

        }

        private SoccerGameDetailItem MapMoreDetailedGameValue(SoccerGameDetailItem soccerItem, GameDetail gameDetail)
        {
            switch (gameDetail.SettlementStatusID)
            {
                case EventStatusID.RefundSettlementStatusID:
                    soccerItem.HomeValue = soccerItem.AwayValue = LangResource.Refund;
                    break;
                case EventStatusID.RunningSettlementStatusID:
                    soccerItem.HomeValue = soccerItem.AwayValue = ConstantValue.Hyphen;
                    break;
                default:
                    MapResourceGameValueByPattern(soccerItem, gameDetail);
                    break;
            }

            return soccerItem;
        }

        private void MapResourceGameValueByPattern(SoccerGameDetailItem item, GameDetail detailedValue)
        {
            switch (item.ResourceID)
            {
                case CricketResourceID.SuperOverWinner:
                case CricketResourceID.TossWinner:
                    item.HomeValue = detailedValue.HomeValue == "H" ? HomeTeam : AwayTeam;
                    item.AwayValue = HomeTeam;
                    break;
                default:
                    if (SportsbookHelper.IsValidResourceId(CricketResourceID.HomeWicketXMethodOfDismissal, item.ResourceID) ||
                        SportsbookHelper.IsValidResourceId(CricketResourceID.AwayWicketXMethodOfDismissal, item.ResourceID))
                    {
                        item.HomeValue = item.AwayValue = detailedValue.HomeValue == "C" ? LangResource.Caught : LangResource.NotCaught;
                    }
                    else
                    {
                        item.HomeValue = detailedValue.HomeValue;
                        item.AwayValue = detailedValue.AwayValue;
                    }

                    break;
            }
        }

        #endregion Utilities
    }
}