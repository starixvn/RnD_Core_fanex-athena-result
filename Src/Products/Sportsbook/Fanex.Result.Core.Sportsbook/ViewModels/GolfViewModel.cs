namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using Base;
    using Base.ViewModels;
    using DTO;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;

    public sealed class GolfViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private const int ScoreWin = 1;
        private const int ScoreLose = 0;

        public GolfViewModel(Sportsbook sportbook, ResultSetting setting)
            : this(sportbook, setting, null)
        {
        }

        public GolfViewModel(Sportsbook sportbook, ResultSetting setting, IRefDataHelper refDataHelper)
            : base(sportbook, setting, refDataHelper)
        {
        }

        public string FinalAwayScore
        {
            get
            {
                if (HasFinalScoreText(SPORTBOOK.FinalHomeScore, SPORTBOOK.FinalAwayScore))
                {
                    return ScoreToText(SPORTBOOK.FinalAwayScore.Value);
                }

                return GetScore(SPORTBOOK.FinalAwayScore);
            }
        }

        public string FinalHomeScore
        {
            get
            {
                if (HasFinalScoreText(SPORTBOOK.FinalHomeScore, SPORTBOOK.FinalAwayScore))
                {
                    return ScoreToText(SPORTBOOK.FinalHomeScore.Value);
                }

                return GetScore(SPORTBOOK.FinalHomeScore);
            }
        }

        private static bool HasFinalScoreText(int? homeScore, int? awayScore)
        {
            if (homeScore.HasValue && awayScore.HasValue)
            {
                return (homeScore.Value.Equals(ScoreWin) && awayScore.Value.Equals(ScoreLose)) ||
                    (homeScore.Value.Equals(ScoreLose) && awayScore.Value.Equals(ScoreWin));
            }

            return false;
        }

        private static string ScoreToText(int score)
            => score == ScoreWin ? LangResource.Won : LangResource.Lose;
    }
}