// -----------------------------------------------------------------------
// <copyright file="LiveCasinoViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.LiveCasino.ViewModels
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using RemotingRef;

    /// <summary>
    /// Live casino view model class.
    /// </summary>
    public sealed class LiveCasinoViewModel : BaseViewModel, IResultViewModel
    {
        private readonly int[] _rouletteBettype = new int[2] { 1102, 1104 };

        private readonly DTO.LiveCasino _liveCasino;

        private string _finalScore;

        private string _result;

        public LiveCasinoViewModel(DTO.LiveCasino livecasino, ResultSetting setting)
            : base(setting)
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

        public override bool IsPending
        {
            get
            {
                return this._liveCasino.IsPending;
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
                   this.VisibleInternalEventStatus);
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
                return ConstantValue.Hyphen;
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

            string[] arrResult = results.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (arrResult.Length == 0)
            {
                return string.Empty;
            }

            var supportedLanguage = this.GetSupportedLanguage;
            var livecasinoResult = string.Join(";", arrResult.Select(i => RefData.GetCasinoBetItem(i, supportedLanguage, string.Empty)));
            if (arrResult.Length < 3)
            {
                livecasinoResult += ";-";
            }

            return livecasinoResult;
        }

        /// <summary>
        /// Get final sore and result.
        /// </summary>
        private void GetFinalScoreAndResult()
        {
            if (this.EventStatus.Equals(ConstantValue.Refund, StringComparison.OrdinalIgnoreCase)
                || this.EventStatus.Equals(ConstantValue.Void, StringComparison.OrdinalIgnoreCase))
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
                            ConstantValue.Hyphen : SetResultsRoulette(this._liveCasino.BetStatus);
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