namespace Fanex.Result.Core.Sportsbook.ViewModels.SoccerDetail
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.Extension;
    using Base.Resources;
    using BaseDetail;
    using DataDomain;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Sportsbook.Resources;
    using Utilities;

    public class CricketResultDetail : BaseResultDetail<SoccerGameDetailItem>
    {
        public CricketResultDetail(
            Sportsbook sportsbook,
            ResultSetting setting,
            IRefDataHelper refDataHelper = null,
            ISportsbookHelperWraper sportsBookHelper = null)
            : base(sportsbook, setting, refDataHelper, sportsBookHelper)
        {
        }

        public override SafeList<SoccerGameDetailItem> GetDetailItems()
        {
            var detailItems = new SafeList<SoccerGameDetailItem>();

            foreach (var group in CricketResourceID.MoreResultGroup)
            {
                foreach (var game in group.Value)
                {
                    detailItems = CreateGameDetailItems(detailItems, group, game);
                }
            }

            return detailItems;
        }

        private SafeList<SoccerGameDetailItem> CreateGameDetailItems(SafeList<SoccerGameDetailItem> detailItems, KeyValuePair<string, List<string>> group, string game)
        {
            var detailGameRecords = Sportsbook.GameDetails
                .Where(i => SportsbookHelper.IsValidResourceId(game, i.ResourceId)).ToSafeList();

            foreach (GameDetail detailItem in detailGameRecords)
            {
                var detailedItemModel = CreateGameDetailItem(group.Key, detailItem);
                detailItems.Add(detailedItemModel);
            }

            return detailItems;
        }

        private SoccerGameDetailItem CreateGameDetailItem(string groupCode, GameDetail detailItem)
        {
            var item = new SoccerGameDetailItem
            {
                GroupCode = groupCode,
                ResourceID = detailItem.ResourceId,
                GroupName = SportsbookHelper.GetResourceLang(groupCode),
                ResourceName = GetResultName(detailItem.BettypeId, LanguageCode, detailItem.ResourceId),
                SettlementStatusID = detailItem.SettlementStatusID
            };

            return MapGameValueByStatusId(item, detailItem);
        }

        protected override void MapGameValueByPattern(SoccerGameDetailItem item, GameDetail detailItem)
        {
            switch (item.ResourceID)
            {
                case CricketResourceID.SuperOverWinner:
                case CricketResourceID.TossWinner:
                case CricketResourceID.HighestOpeningPartnership:
                case CricketResourceID.HighestScoreInOver:
                    var values = new Dictionary<string, string>
                    {
                        { "h", HomeTeam},
                        { "a", AwayTeam},
                        { "n", LangResource.None},
                    };
                    var key = detailItem.HomeValue.ToLowerInvariant();
                    var homeValue = values.ContainsKey(key) ? values[key] : detailItem.HomeValue;                    
                    item.HomeValue = homeValue;
                    item.AwayValue = HomeTeam;
                    break;
                default:
                    MapResourceGameValueLikePattern(detailItem, item);
                    break;
            }
        }
        private void MapResourceGameValueLikePattern(GameDetail detailItem, SoccerGameDetailItem item)
        {
            if (SportsbookHelper.IsValidResourceId(CricketResourceID.HomeWicketXMethodOfDismissal6Way, item.ResourceID) ||
                SportsbookHelper.IsValidResourceId(CricketResourceID.AwayWicketXMethodofDismissal6Way, item.ResourceID))
            {
                var dismissalValue = new Dictionary<string, string>
                        {
                            { "c", SportsbookResx.Caught },
                            { "b",  SportsbookResx.Bowled },
                            { "lbw", SportsbookResx.LBW },
                            { "ro", SportsbookResx.RunOut },
                            { "s", SportsbookResx.Stumped },
                            { "ot", SportsbookResx.Others }
                        };
                var homevalue = detailItem.HomeValue.ToLowerInvariant();
                item.HomeValue = item.AwayValue = dismissalValue.ContainsKey(homevalue) ? dismissalValue[homevalue] : homevalue;
            }
            else if (SportsbookHelper.IsValidResourceId(CricketResourceID.HomeWicketXMethodOfDismissal, item.ResourceID) ||
                        SportsbookHelper.IsValidResourceId(CricketResourceID.AwayWicketXMethodOfDismissal, item.ResourceID))
            {
                item.HomeValue = item.AwayValue = detailItem.HomeValue == "C" ? LangResource.Caught : LangResource.NotCaught;
            }
            else if (CricketResourceID.BatsmanResourceList.Any(i => SportsbookHelper.IsValidResourceId(i, item.ResourceID)))
            {
                item.HomeValue = RefDataHelper.GetTeamName(detailItem.OtherInfo, LanguageCode, string.Empty);
                item.AwayValue = detailItem.HomeValue;
            }
            else if (CricketResourceID.BatsmansResourceList.Any(i => SportsbookHelper.IsValidResourceId(i, item.ResourceID)))
            {
                var ids = detailItem.OtherInfo.Split(',');
                var names = new List<string>();
                foreach (var id in ids)
                {
                    names.Add(RefDataHelper.GetTeamName(id.Trim(), LanguageCode, string.Empty));
                }
                item.HomeValue = string.Join(", ", names);
            }
            else if (CricketResourceID.YesNoResourceList.Any(i => SportsbookHelper.IsValidResourceId(i, item.ResourceID)))
            {
                item.HomeValue = detailItem.HomeValue.ToLowerInvariant() == "y" ? LangResource.Yes : LangResource.No;
            }
            else if (CricketResourceID.ManOutResourceList.Any(i => SportsbookHelper.IsValidResourceId(i, item.ResourceID)))
            {
                var ids = detailItem.HomeValue.Split(',');
                var names = new List<string>();
                foreach (var id in ids)
                {
                    names.Add(RefDataHelper.GetTeamName(id.Trim(), LanguageCode, string.Empty));
                }                
                item.HomeValue = string.Join(" & ", names);
                item.AwayValue = string.IsNullOrEmpty(detailItem.OtherInfo)?
                    LangResource.None:
                    RefDataHelper.GetTeamName(detailItem.OtherInfo, LanguageCode, string.Empty);
            }
            else
            {
                item.HomeValue = detailItem.HomeValue;
                item.AwayValue = detailItem.AwayValue;
            }
        }

        private string GetResultName(int betType, string languageCode, string resourceId)
        {
            const int minLength = 8;
            const int firstIndexResourceId = 5;
            betType = GetBetTypeFromResourceId(betType, resourceId);
            var id = BetTypeID.CricketBettypeGroup.ContainsKey(betType) ? BetTypeID.CricketBettypeGroup[betType].ToString() : resourceId;

            var betId = string.Empty;
            if (resourceId.Length >= minLength)
            {
                betId = resourceId.Substring(firstIndexResourceId);
            }

            return RefDataHelper.GetBetTypeNameById(id, languageCode, betId);
        }

        private static int GetBetTypeFromResourceId(int betType, string resourceId)
        {
            const int maxLengthOfBettypeCricket = 4;
            if (betType == 0 && resourceId.Length >= maxLengthOfBettypeCricket)
            {
                return int.Parse(resourceId.Substring(0, maxLengthOfBettypeCricket));
            }

            return betType;
        }
    }
}