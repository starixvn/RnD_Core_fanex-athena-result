// -----------------------------------------------------------------------
// <copyright file="GoldDeluxeViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.GoldDeluxe.ViewModels
{
    using System;
    using System.Globalization;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.GoldDeluxe.DataDomain;
    using Fanex.Result.Core.GoldDeluxe.DTO;
    using Fanex.Result.Core.GoldDeluxe.Utilities;
    using RemotingRef;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GoldDeluxeViewModel : BaseViewModel, IResultViewModel
    {
        private readonly GoldDeluxe _goldDeluxe;
        private string _result;
        private string _gameId;
        private string _htmlResult;

        public GoldDeluxeViewModel(GoldDeluxe goldDeluxe, ResultSetting setting)
            : base(setting)
        {
            this._goldDeluxe = goldDeluxe;
            this.LanguageCode = setting.LanguageCode;
            this.Sport = setting.SportType;
        }

        public override string LeagueName
        {
            get
            {
                return RefData.GetBetTypeName(
               this._goldDeluxe.Bettype.ToString(CultureInfo.InvariantCulture),
               this.GetSupportedLanguage,
               string.Empty);
            }
        }

        public override string EventStatus
        {
            get
            {
                return LangResource.Completed;
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

        public string ResultInExcel
        {
            get
            {
                if (string.IsNullOrEmpty(_result))
                {
                    BindingResult();
                }

                return _result;
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
            if (!string.IsNullOrEmpty(this._goldDeluxe.Result))
            {
                var resultTokens = this._goldDeluxe.Result.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (resultTokens.Length >= 2)
                {
                    _gameId = GetGameId(resultTokens[0]);
                    _result = GetDetailedGameResult(resultTokens[1]);
                }
            }

            if (!string.IsNullOrEmpty(_result))
            {
                _htmlResult = this._goldDeluxe.Bettype == GoldDeluxeBettype.Roulette ? GoldDeluxeHelper.GetRoutletResult(_result)
                   : GoldDeluxeHelper.GetBacaratResult(_result, LangResource.Banker, LangResource.Player);
            }
            else
            {
                _htmlResult = LangResource.NoInfo;
            }
        }
    }
}