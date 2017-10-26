namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System.Linq;
    using Base;
    using Base.Resources;
    using Base.ViewModels;
    using DataDomain;
    using DTO;

    public sealed class MuayThaiViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private int _isValidScore = -1;

        public MuayThaiViewModel(Sportsbook sportbook, ResultSetting setting)
            : base(sportbook, setting)
        {
        }

        public bool IsValidScore
        {
            get
            {
                if (_isValidScore < 0)
                {
                    _isValidScore = ScoreHasMuayThaiValue()
                           && (SPORTBOOK.FinalHomeScore.Value + SPORTBOOK.FinalAwayScore.Value > 0) ? 1 : 0;
                }

                return _isValidScore > 0;
            }
        }

        public string FinalAwayScore
        {
            get
            {
                if (IsValidScore)
                {
                    return GetFinalScore(SPORTBOOK.FinalAwayScore.Value - SPORTBOOK.FinalHomeScore.Value);
                }

                return GetScore(SPORTBOOK.FinalAwayScore);
            }
        }

        public string FinalHomeScore
        {
            get
            {
                if (this.IsValidScore)
                {
                    return GetFinalScore(SPORTBOOK.FinalHomeScore.Value - SPORTBOOK.FinalAwayScore.Value);
                }

                return GetScore(SPORTBOOK.FinalHomeScore);
            }
        }

        public string KOInRound
        {
            get
            {
                if (IsValidScore && EventStatus.Equals(LangResource.Completed, System.StringComparison.OrdinalIgnoreCase))
                {
                    if (SPORTBOOK.FinalHomeScore.Value == 5 || SPORTBOOK.FinalAwayScore.Value == 5)
                    {
                        return LangResource.FiveRoundsCompleted;
                    }

                    if (SPORTBOOK.FinalHomeScore.Value != SPORTBOOK.FinalAwayScore.Value
                        && !string.IsNullOrEmpty(SPORTBOOK.BetStatus))
                    {
                        return string.Format(LangResource.KOInRound, this.SPORTBOOK.BetStatus.Replace(";", string.Empty));
                    }
                }

                return string.Empty;
            }
        }

        protected override string GetWonTeam(bool isHome)
        {
            if (IsValidScore)
            {
                return base.GetWonTeam(isHome);
            }

            return string.Empty;
        }

        private static string GetFinalScore(int keyScore)
        {
            return keyScore == 0 ? LangResource.Draw : (keyScore > 0 ? LangResource.Won : LangResource.Lose);
        }

        private bool ScoreHasMuayThaiValue()
        {
            return SPORTBOOK.FinalHomeScore.HasValue && SPORTBOOK.FinalAwayScore.HasValue &&
                GameResources.PossibleMuayThaiScore.Contains(SPORTBOOK.FinalHomeScore.Value) &&
                GameResources.PossibleMuayThaiScore.Contains(SPORTBOOK.FinalAwayScore.Value);
        }
    }
}