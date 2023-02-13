namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using Base;
    using Base.ViewModels;
    using DTO;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Sportsbook.Resources;
    using System;
    using System.Collections.Generic;

    public sealed class GolfViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private const int ScoreWin = 0;
        private const int ScoreLose = 1;
        private const int ScoreRetire = 999;
        

        public GolfViewModel(Sportsbook sportbook, ResultSetting setting)
            : this(sportbook, setting, null)
        {
        }

        public GolfViewModel(Sportsbook sportbook, ResultSetting setting, IRefDataHelper refDataHelper)
            : base(sportbook, setting, refDataHelper)
        {
        }

        public string FinalAwayScore
            => CalculateScore(SPORTBOOK.FinalAwayScore);

        public string FinalHomeScore
            => CalculateScore(SPORTBOOK.FinalHomeScore);

        protected override string GetScore(int? score)
        {
            if (HasFinalScoreText(score))
            {
                return ScoreToText(score.Value);
            }

            return base.GetScore(score);
        }

        private bool HasFinalScoreText(int? score)
        {
            if (score.HasValue && score.Value.Equals(ScoreRetire))
            {
                return true;
            }

            if (SPORTBOOK.FinalHomeScore.HasValue && SPORTBOOK.FinalAwayScore.HasValue)
            {
                var sumScore = SPORTBOOK.FinalHomeScore + SPORTBOOK.FinalAwayScore;
                return sumScore.Equals(ScoreLose);
            }

            return false;
        }

        private string CalculateScore(int? score)
        {
            if (SPORTBOOK.BetStatus.IndexOf(ConstantValue.Refund, StringComparison.OrdinalIgnoreCase) >= 0 ||
                SPORTBOOK.BetStatus.IndexOf(LangResource.Abandoned1h, StringComparison.OrdinalIgnoreCase) >= 0 ||
                SPORTBOOK.BetStatus.IndexOf(LangResource.Abandoned2h, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return LangResource.Refund;
            }

            return GetScore(score);
        }

        private static string ScoreToText(int score)
        {
            var SpecialScore = new Dictionary<int, string>
            {
                {ScoreWin, LangResource.Won},
                {ScoreLose, LangResource.Lose},
                {ScoreRetire, SportsbookResx.Dnf}
            };

            return SpecialScore[score];
        }
    }
}