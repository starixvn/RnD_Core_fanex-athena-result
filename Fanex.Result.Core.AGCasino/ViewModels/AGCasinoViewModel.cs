// -----------------------------------------------------------------------
// <copyright file="AGCasinoViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.AGCasino.ViewModels
{
    using System;
    using System.Globalization;
    using Fanex.Result.Core.AGCasino.DataDomain;
    using Fanex.Result.Core.AGCasino.DTO;
    using Fanex.Result.Core.AGCasino.Utilities;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.ViewModels;
    using RemotingRef;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AGCasinoViewModel : BaseViewModel, IResultViewModel
    {
        private readonly AGCasino _agCasino;
        private string _result;
        private string _gameId;
        private string _htmlResult;

        public AGCasinoViewModel(AGCasino agCasino, ResultSetting setting, LangResources resource)
            : base(setting, resource)
        {
            this._agCasino = agCasino;
            this.LanguageCode = setting.LanguageCode;
            this.Sport = setting.SportType;
        }

        public override string LeagueName
        {
            get
            {
                return RefData.GetBetTypeName(
               this._agCasino.Bettype.ToString(CultureInfo.InvariantCulture),
               this.GetSupportedLanguage,
               string.Empty);
            }
        }

        public override string EventStatus
        {
            get
            {
                return Lang.Completed;
            }
        }

        public string GameID
        {
            get
            {
                if (string.IsNullOrEmpty(_gameId))
                {
                    BindingResult();
                }

                return _gameId;
            }
        }

        public string ResultInHtml
        {
            get
            {
                if (string.IsNullOrEmpty(_htmlResult))
                {
                    BindingResult();
                }

                return _htmlResult;
            }
        }

        private static string GetGameId(string gameToken)
        {
            var gameTokens = gameToken.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            return gameTokens.Length >= 2 ? gameTokens[1] : string.Empty;
        }

        private static string GetDetailedGameResult(string gameDetailedTokens)
        {
            var detailResultToken = gameDetailedTokens.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            return detailResultToken.Length >= 2 ? detailResultToken[1] : string.Empty;
        }

        private void BindingResult()
        {
            if (!string.IsNullOrEmpty(this._agCasino.Result))
            {
                var resultTokens = this._agCasino.Result.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (resultTokens.Length >= 2)
                {
                    _gameId = GetGameId(resultTokens[0]);
                    _result = GetDetailedGameResult(resultTokens[1]);
                }
            }

            if (!string.IsNullOrEmpty(_result))
            {
                _htmlResult = this._agCasino.Bettype == AGCasinoBettype.Roulette ? AGCasinoHelper.GetRoutletResult(_result)
                   : AGCasinoHelper.GetBacaratResult(_result, Lang.Banker, Lang.Player);
            }
            else
            {
                _htmlResult = Lang.NoInfo;
            }
        }
    }
}