// -----------------------------------------------------------------------
// <copyright file="LiveCasinoViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Utilities;
    using RemotingRef;

    /// <summary>
    /// Live casino view model class.
    /// </summary>
    public sealed class LiveCasinoViewModel : BaseViewModel,IResultViewModel
    {
        private readonly int[] _rouletteBettype = new int[2] { 1102, 1104 };

        private readonly LiveCasino _liveCasino;

        private string _finalScore;

        private string _result;

        public LiveCasinoViewModel(LiveCasino livecasino, ResultSetting setting, LangResources resource)
            : base(setting, resource)
        {
            this._liveCasino = livecasino;
            this.LanguageCode = setting.LanguageCode;
            this.Sport = setting.SportType;
        }

        public override int LeagueId
        {
            get
            {
                return this._liveCasino.LeagueId;
            }
        }

        public override string LeagueName
        {
            get
            {
                return this.GetLeagueNameByLanguage(this._liveCasino.LeagueId);
            }
        }

        public override string KickOffTime
        {
            get
            {
                return this.GetFormatedKickOffTime(this._liveCasino.KickOffTime);
            }
        }

        public string PlayerAndBanker
        {
            get
            {
                if (_rouletteBettype.Contains(this._liveCasino.Bettype))
                {
                    return string.Empty;
                }

                return "P - B";
            }
        }

        public string GameType
        {
            get
            {
                return this._liveCasino.ShowTime;
            }
        }

        public string MatchCode
        {
            get
            {
                return this._liveCasino.MatchCode;
            }
        }

        public string FinalScore
        {
            get
            {
                if (_finalScore == null)
                {
                    this.GetFinalScoreAndResult();
                }

                return this._finalScore;
            }
        }

        public string Result
        {
            get
            {
                if (_result == null)
                {
                    this.GetFinalScoreAndResult();
                }

                return this._result;
            }
        }

        public override string EventStatus
        {
            get
            {
                return MapEventStatus.MapEventStatusString(
                    this._liveCasino.EventStatus,
                    this._liveCasino.IsPending,
                    this._liveCasino.IsHTCal,
                    this._liveCasino.Calculate,
                    this.Lang);
            }
        }

        /// <summary>
        /// Get status of game.
        /// </summary>
        /// <param name="results">String value from database.</param>
        /// <returns>Return string result of a game.</returns>
        private string SetResultsRoulette(string results)
        {
            if (results.Trim().Length < 1)
            {
                return StringResources.HYPHEN;
            }

            string[] arr = results.Split(',');
            string str = string.Empty, statusName;

            for (int i = 0; i < arr.Length; i++)
            {
                statusName = RefData.GetRouletteBetItem(arr[i], this.GetSupportedLanguage, string.Empty);

                if (i < arr.Length - 1)
                {
                    str += string.Format("{0}; ", statusName);
                }
                else
                {
                    str += statusName;
                }

                if ((i + 1) % 3 == 0)
                {
                    str += "<br/>";
                }
            }

            return str;
        }

        /// <summary>
        /// Set result.
        /// </summary>
        /// <param name="results">The results getting from DB.</param>
        /// <returns>The result string.</returns>
        private string SetResults(string results)
        {
            if (results.Trim().Length < 1)
            {
                return string.Empty;
            }

            string[] arr = results.Split(',');
            string str = string.Empty, statusName = string.Empty;

            for (int i = 0; i < arr.Length; i++)
            {
                statusName = RefData.GetCasinoBetItem(arr[i], this.GetSupportedLanguage, string.Empty);

                if (i < arr.Length - 1)
                {
                    if (i > 1)
                    {
                        str += string.Format("{0}, ", statusName);
                    }
                    else
                    {
                        str += string.Format("{0}; ", statusName);
                    }
                }
                else
                {
                    str += statusName;
                }
            }

            if (arr.Length < 3)
            {
                str += string.Format("{0}; -", statusName);
            }

            return str;
        }

        /// <summary>
        /// Get final sore and result.
        /// </summary>
        private void GetFinalScoreAndResult()
        {
            if (this.EventStatus.Equals(StringResources.REFUND, StringComparison.OrdinalIgnoreCase)
                || this.EventStatus.Equals(StringResources.VOID, StringComparison.OrdinalIgnoreCase))
            {
                this._result = this._finalScore = this.EventStatus;
            }
            else
            {
                if (_rouletteBettype.Contains(this._liveCasino.Bettype))
                {
                    if (this._liveCasino.FinalHomeScore.HasValue)
                    {
                        this._finalScore = this._liveCasino.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture);
                        this._result = this._liveCasino.FinalHomeScore.Value == 0 ?
                            SetResultsRoulette(this._liveCasino.BetStatus) : StringResources.HYPHEN;
                    }
                    else
                    {
                        this._result = this._finalScore = string.Empty;
                    }
                }
                else
                {
                    this._finalScore = this._liveCasino.FinalHomeScore.HasValue ?
                       this._liveCasino.FinalHomeScore.Value + " - " +

                       (this._liveCasino.FinalAwayScore.HasValue ? this._liveCasino.FinalAwayScore.ToString() : string.Empty)
                       :
                       string.Empty;
                    this._result = SetResults(this._liveCasino.BetStatus);
                }
            }
        }
    }
}