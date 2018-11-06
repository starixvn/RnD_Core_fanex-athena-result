namespace Fanex.Result.Core.GoldDeluxe.ViewModels
{
    using System;
    using System.Globalization;
    using Base;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using DataDomain;
    using DTO;
    using Utilities;

    public class GoldDeluxeViewModel : BaseViewModel, IResultViewModel
    {
        private readonly GoldDeluxe _goldDeluxe;
        private string _result;
        private string _gameId;
        private string _htmlResult;

        public GoldDeluxeViewModel(GoldDeluxe goldDeluxe, ResultSetting setting)
            : this(goldDeluxe, setting, null)
        {
        }

        public GoldDeluxeViewModel(GoldDeluxe goldDeluxe, ResultSetting setting, IRefDataHelper refDataHelper)
           : base(setting, refDataHelper)
        {
            _goldDeluxe = goldDeluxe;
            LanguageCode = setting.LanguageCode;
            Sport = setting.SportType;
        }

        public override string LeagueName
            => RefDataHelper.GetBetTypeNameById(
                   _goldDeluxe.Bettype.ToString(CultureInfo.InvariantCulture),
                   GetSupportedLanguage,
                   string.Empty);

        public override string EventStatus => LangResource.Completed;

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
            var gameTokens = gameToken.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            return gameTokens.Length >= 2 ? gameTokens[1] : string.Empty;
        }

        private static string GetDetailedGameResult(string gameDetailedTokens)
        {
            var detailResultToken = gameDetailedTokens.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            return detailResultToken.Length >= 2 ? detailResultToken[1] : string.Empty;
        }

        private void BindingResult()
        {
            if (!string.IsNullOrEmpty(_goldDeluxe.Result))
            {
                var resultTokens = _goldDeluxe.Result.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                if (resultTokens.Length >= 2)
                {
                    _gameId = GetGameId(resultTokens[0]);
                    _result = GetDetailedGameResult(resultTokens[1]);
                }
            }

            if (string.IsNullOrEmpty(_result))
            {
                _htmlResult = LangResource.NoInfo;
                return;
            }

            switch(_goldDeluxe.Bettype)
            {
                case GoldDeluxeBettype.Roulette:
                    _htmlResult = GoldDeluxeHelper.GetRoutletResult(_result);
                    return;

                case GoldDeluxeBettype.Sicbo:
                    _htmlResult = GoldDeluxeHelper.GetSicboResult(_result);
                    return;

                default:
                    _htmlResult = GoldDeluxeHelper.GetBacaratResult(_result, LangResource.Banker, LangResource.Player);
                    return;
            }
        }
    }
}