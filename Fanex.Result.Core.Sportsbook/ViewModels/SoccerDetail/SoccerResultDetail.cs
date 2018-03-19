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
    using Resources;
    using Utilities;

    public class SoccerResultDetail : BaseResultDetail<SoccerGameDetailItem>
    {
        public SoccerResultDetail(
            Sportsbook sportsbook,
            ResultSetting setting,
            ISportsbookRefDataHelper refDataHelper = null,
            ISportsbookHelperWraper sportsBookHelper = null)
            : base(sportsbook, setting, refDataHelper, sportsBookHelper)
        {
        }

        public override SafeList<SoccerGameDetailItem> GetDetailItems()
        {
            var detailItems = new SafeList<SoccerGameDetailItem>();

            foreach (var group in GameResources.MoreResultGroup)
            {
                foreach (var game in group.Value)
                {
                    detailItems = CreateGameDetailItems(detailItems, group, game);
                }
            }

            return detailItems;
        }

        protected override void MapGameValueByPattern(SoccerGameDetailItem item, GameDetail detailItem)
        {
            if (item.ResourceID == GameResourceID.TimeOfFirstGoal)
            {
                var result = detailItem.HomeValue;

                if (result == "NG")
                {
                    item.HomeValue = LangResource.NoGoal;
                }
                else
                {
                    var firstTimeValue = result.Substring(0, result.IndexOf('-'));
                    var secondTimeValue = result.Replace(firstTimeValue + "-", string.Empty);
                    var firstTimeText = Convert.ToInt32(firstTimeValue) - 1 + (firstTimeValue.Length == 1 ? "0" : string.Empty) + ":01";
                    var secondTimeText = secondTimeValue != "90" ?
                        (secondTimeValue + (secondTimeValue.Length == 1 ? "0" : string.Empty) + ":00") :
                        SportsbookResx.FT;

                    item.HomeValue = $"{firstTimeText}-{secondTimeText}";
                }
            }
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
                ResourceName = SportsbookHelper.GetResourceLang(detailItem.ResourceId)
            };

            return MapGameValueByStatusId(item, detailItem);
        }
    }
}