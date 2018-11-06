namespace Fanex.Result.Core.LiveCasino.ViewModels
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Base;
    using Base.DataDomain;
    using Base.Utilities;
    using Base.ViewModels;

    public sealed class LiveCasinoViewModel : BaseViewModel, IResultViewModel
    {
        private readonly int[] _rouletteBettype = { 1102, 1104 };

        private readonly DTO.LiveCasino _liveCasino;

        private string _finalScore;

        private string _result;

        public LiveCasinoViewModel(DTO.LiveCasino livecasino, ResultSetting setting)
            : base(setting)
        {
            _liveCasino = livecasino;
            LanguageCode = setting.LanguageCode;
            Sport = setting.SportType;
        }

        public override int LeagueId
        {
            get
            {
                return _liveCasino.LeagueId;
            }
        }

        public override string LeagueName
        {
            get
            {
                return GetLeagueNameByLanguage(_liveCasino.LeagueId);
            }
        }

        public override string KickOffTime
        {
            get
            {
                return GetFormatedKickOffTime(_liveCasino.KickOffTime);
            }
        }

        public override bool IsPending
        {
            get
            {
                return _liveCasino.IsPending;
            }
        }

        public string PlayerAndBanker
        {
            get
            {
                if (_rouletteBettype.Contains(_liveCasino.Bettype))
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
                return _liveCasino.ShowTime;
            }
        }

        public string MatchCode
        {
            get
            {
                return _liveCasino.MatchCode;
            }
        }

        public string FinalScore
        {
            get
            {
                if (_finalScore == null)
                {
                    GetFinalScoreAndResult();
                }

                return _finalScore;
            }
        }

        public string Result
        {
            get
            {
                if (_result == null)
                {
                    GetFinalScoreAndResult();
                }

                return _result;
            }
        }

        public override string EventStatus
        {
            get
            {
                return MapEventStatus.MapEventStatusString(
                    _liveCasino.EventStatus,
                    _liveCasino.IsPending);
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
            string statusName;
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < arr.Length; i++)
            {
                statusName = RefDataHelper.GetRouletteBetItem(arr[i], GetSupportedLanguage, string.Empty);

                stringBuilder.Append(i < arr.Length - 1 ? string.Format("{0}; ", statusName) : statusName);

                if ((i + 1) % 3 == 0)
                {
                    stringBuilder.Append("<br/>");
                }
            }

            return stringBuilder.ToString();
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

            string[] arrResult = results.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (arrResult.Length == 0)
            {
                return string.Empty;
            }

            var supportedLanguage = GetSupportedLanguage;
            var livecasinoResult = string.Join(";", arrResult.Select(i => RefDataHelper.GetCasinoBetItem(i, supportedLanguage, string.Empty)));
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
            if (EventStatus.Equals(ConstantValue.Refund, StringComparison.OrdinalIgnoreCase)
                || EventStatus.Equals(ConstantValue.Void, StringComparison.OrdinalIgnoreCase))
            {
                _result = _finalScore = EventStatus;
            }
            else
            {
                GenerateResult();
            }
        }

        private void GenerateResult()
        {
            if (_rouletteBettype.Contains(_liveCasino.Bettype))
            {
                if (_liveCasino.FinalHomeScore.HasValue)
                {
                    _finalScore = _liveCasino.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture);
                    _result = _liveCasino.FinalHomeScore.Value == 0 ?
                        ConstantValue.Hyphen : SetResultsRoulette(_liveCasino.BetStatus);
                }
                else
                {
                    _result = _finalScore = string.Empty;
                }
            }
            else
            {
                _finalScore = _liveCasino.FinalHomeScore.HasValue ?
                   _liveCasino.FinalHomeScore.Value + " - " +

                   (_liveCasino.FinalAwayScore.HasValue ? _liveCasino.FinalAwayScore.ToString() : string.Empty)
                   :
                   string.Empty;

                _result = SetResults(_liveCasino.BetStatus);
            }
        }
    }
}