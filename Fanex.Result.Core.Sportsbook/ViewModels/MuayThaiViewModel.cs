namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System.Linq;
    using Base;
    using Base.Resources;
    using Base.ViewModels;
    using DataDomain;
    using DTO;
    using Utilities;

    public sealed class MuayThaiViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private int _isValidScore = -1;

        public MuayThaiViewModel(Sportsbook sportbook, ResultSetting setting)
            : this(sportbook, setting, null)
        {
        }

        public MuayThaiViewModel(Sportsbook sportbook, ResultSetting setting, ISportsbookRefDataHelper refDataHelper)
           : base(sportbook, setting, refDataHelper)
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
             => IsValidScore ?
            GetFinalScore(SPORTBOOK.FinalAwayScore.Value - SPORTBOOK.FinalHomeScore.Value) :
            GetScore(SPORTBOOK.FinalAwayScore);

        public string FinalHomeScore
           => IsValidScore ?
            GetFinalScore(SPORTBOOK.FinalHomeScore.Value - SPORTBOOK.FinalAwayScore.Value) :
            GetScore(SPORTBOOK.FinalHomeScore);

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
                        return string.Format(LangResource.KOInRound, SPORTBOOK.BetStatus.Replace(";", string.Empty));
                    }
                }

                return string.Empty;
            }
        }

        protected internal override string GetWonTeam(bool isHome)
            => IsValidScore ? base.GetWonTeam(isHome) : string.Empty;

        private static string GetFinalScore(int keyScore)
            => keyScore == 0 ? LangResource.Draw : (keyScore > 0 ? LangResource.Won : LangResource.Lose);

        private bool ScoreHasMuayThaiValue()
            => SPORTBOOK.FinalHomeScore.HasValue && SPORTBOOK.FinalAwayScore.HasValue &&
                GameResources.PossibleMuayThaiScore.Contains(SPORTBOOK.FinalHomeScore.Value) &&
                GameResources.PossibleMuayThaiScore.Contains(SPORTBOOK.FinalAwayScore.Value);
    }
}