namespace Fanex.Result.Core.Sportsbook.ViewModels.SoccerDetail
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.Extension;
    using Base.Resources;
    using BaseDetail;
    using DataDomain;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
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
                    item.HomeValue = detailItem.HomeValue == "H" ? HomeTeam : AwayTeam;
                    item.AwayValue = HomeTeam;
                    break;
                default:
                    if (SportsbookHelper.IsValidResourceId(CricketResourceID.HomeWicketXMethodOfDismissal, item.ResourceID) ||
                        SportsbookHelper.IsValidResourceId(CricketResourceID.AwayWicketXMethodOfDismissal, item.ResourceID))
                    {
                        item.HomeValue = item.AwayValue = detailItem.HomeValue == "C" ? LangResource.Caught : LangResource.NotCaught;
                    }
                    else
                    {
                        item.HomeValue = detailItem.HomeValue;
                        item.AwayValue = detailItem.AwayValue;
                    }

                    break;
            }
        }

        private string GetResultName(int betType, string languageCode, string resourceId)
        {
            const int minLength = 8;
            const int firstIndexResourceId = 5;
            var id = BetTypeID.CricketBettypeGroup.ContainsKey(betType) ? BetTypeID.CricketBettypeGroup[betType].ToString() : resourceId;
            var betId = string.Empty;
            if (resourceId.Length >= minLength)
            {
                betId = resourceId.Substring(firstIndexResourceId);
            }

            return RefDataHelper.GetBetTypeNameById(id, languageCode, betId);
        }
    }
}