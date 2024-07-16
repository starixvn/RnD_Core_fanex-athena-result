namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Globalization;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using Base.ViewModels;
    using DTO;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Baseball.DTO;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Fanex.Result.Core.Volleyball.DTO;

    public sealed class BaseBallViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        readonly DateTime cutOfDate = new DateTime(2024, 8, 1);

        public BaseBallViewModel(Sportsbook sportbook, ResultSetting setting)
            : this(sportbook, setting, null)
        {
        }

        public BaseBallViewModel(Sportsbook sportbook, ResultSetting setting, IRefDataHelper refDataHelper)
           : base(sportbook, setting, refDataHelper)
        {
            ResultDetailItems = new MapBaseball(sportbook, setting, refDataHelper, null).GetDetailItems();
            MoreResultDetail = ResultDetailItems
            .Where(i => MapBaseballResultMetadata.SupportBettype(i.BetTypeId))
            .OrderBy(i => MapBaseballResultMetadata.GetMapByBettypeId(i.BetTypeId).Order)
            .ToList();
        }

        public SafeList<MapBaseballResult> ResultDetailItems { get; set; }

        public List<MapBaseballResult> MoreResultDetail { get; }

        public override bool VisibleDetailedLink => base.VisibleDetailedLink && MoreResultDetail.Any();

        public override bool VisibleGameDetail => MoreResultDetail.Any();

        public string AwayExtraTime => UsingOldData() ?
            GetOverTimeScore(false) :
            Score(BaseballResourceID.ExtraTime, isHome: false);

        public string HomeExtraTime => UsingOldData() ?
            GetOverTimeScore(true) :
            Score(BaseballResourceID.ExtraTime, isHome: true);

        public string HomeHits => Score(BaseballResourceID.Hits, isHome: true);

        public string AwayHits => Score(BaseballResourceID.Hits, isHome: false);

        public string FinalAwayScore => UsingOldData() ?
            GetScore(SPORTBOOK.FinalAwayScore) :
            Score(BaseballResourceID.FinalScore, isHome: false);

        public string FinalHomeScore => UsingOldData() ?
            GetScore(SPORTBOOK.FinalHomeScore) :
            Score(BaseballResourceID.FinalScore, isHome: true);

        public override string GetHomeAwaySet(int index, bool isHome)
        => UsingOldData() ?
            GetHomeAwaySetByOldData(index - 1, isHome) :
            Score($"010{index}", isHome);

        private string GetHomeAwaySetByOldData(int index, bool isHome)
        {
            const int maxIndex = 9;
            if (index > maxIndex)
            {
                return ConstantValue.Hyphen;
            }

            if (HomeSet == null || AwaySet == null)
            {
                HomeSet = new string[maxIndex + 1];
                AwaySet = new string[maxIndex + 1];

                for (int j = 0; j < maxIndex; j++)
                {
                    HomeSet[j] = ConstantValue.Hyphen;
                    AwaySet[j] = ConstantValue.Hyphen;
                }

                HomeSet[maxIndex] = AwaySet[maxIndex] = "-1";

                GenerateHomeAwaySetValueFromBetStatus(maxIndex + 1);
            }

            return isHome ? HomeSet[index] : AwaySet[index];
        }

        private string GetOverTimeScore(bool isHome)
        {
            var scoreText = GetHomeAwaySet(10, isHome);

            if (!string.IsNullOrEmpty(scoreText) && scoreText.All(char.IsDigit))
            {
                var score = Convert.ToInt32(scoreText);

                return score >= 0 ? score.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
            }

            return ConstantValue.Hyphen;
        }

        private string Score(string resourceId, bool isHome)
        {
            var item = ResultDetailItems.FirstOrDefault(i => i.ResourceID == resourceId);
            if (item == null)
            {
                return ConstantValue.Hyphen;
            }

            return isHome ? item.HomeValue : item.AwayValue;
        }

        private bool UsingOldData()
        => SPORTBOOK.EventDate < cutOfDate;
    }
}