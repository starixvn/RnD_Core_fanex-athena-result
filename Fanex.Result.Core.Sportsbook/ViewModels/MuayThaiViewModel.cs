// -----------------------------------------------------------------------
// <copyright file="MuayThaiViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.DataDomain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class MuayThaiViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private int _isValidScore = -1;

        public MuayThaiViewModel(DTO.Sportsbook sportbook, ResultSetting setting)
            : base(sportbook, setting)
        {
        }

        public bool IsValidScore
        {
            get
            {
                if (_isValidScore < 0)
                {
                    _isValidScore = this.SPORTBOOK.FinalHomeScore.HasValue && this.SPORTBOOK.FinalAwayScore.HasValue
                           && GameResources.PossibleMuayThaiScore.Contains(this.SPORTBOOK.FinalHomeScore.Value)
                           && GameResources.PossibleMuayThaiScore.Contains(this.SPORTBOOK.FinalAwayScore.Value)
                           && (this.SPORTBOOK.FinalHomeScore.Value + this.SPORTBOOK.FinalAwayScore.Value > 0) ? 1 : 0;
                }

                return _isValidScore > 0 ? true : false;
            }
        }

        public string FinalAwayScore
        {
            get
            {
                if (this.IsValidScore)
                {
                    return GetFinalScore(this.SPORTBOOK.FinalAwayScore.Value - this.SPORTBOOK.FinalHomeScore.Value);
                }

                return this.SPORTBOOK.FinalAwayScore.HasValue ? this.SPORTBOOK.FinalAwayScore.Value.ToString() : ConstantValue.Hyphen;
            }
        }

        public string FinalHomeScore
        {
            get
            {
                if (this.IsValidScore)
                {
                    return GetFinalScore(this.SPORTBOOK.FinalHomeScore.Value - this.SPORTBOOK.FinalAwayScore.Value);
                }

                return this.SPORTBOOK.FinalHomeScore.HasValue ? this.SPORTBOOK.FinalHomeScore.Value.ToString() : ConstantValue.Hyphen;
            }
        }

        public string KOInRound
        {
            get
            {
                if (this.IsValidScore && this.EventStatus.Equals(LangResource.Completed, System.StringComparison.OrdinalIgnoreCase))
                {
                    if (this.SPORTBOOK.FinalHomeScore.Value == 5 || this.SPORTBOOK.FinalAwayScore.Value == 5)
                    {
                        return LangResource.FiveRoundsCompleted;
                    }

                    if (this.SPORTBOOK.FinalHomeScore.Value != this.SPORTBOOK.FinalAwayScore.Value
                        && !string.IsNullOrEmpty(this.SPORTBOOK.BetStatus))
                    {
                       return string.Format(LangResource.KOInRound, this.SPORTBOOK.BetStatus);
                    }
                }

                return string.Empty;
            }
        }

        protected override string GetWonTeam(bool isHome)
        {
            if (this.IsValidScore)
            {
                return base.GetWonTeam(isHome);
            }

            return string.Empty;
        }

        private string GetFinalScore(int keyScore)
        {
            return keyScore == 0 ? LangResource.Draw : (keyScore > 0 ? LangResource.Win : LangResource.Lose);
        }
    }
}