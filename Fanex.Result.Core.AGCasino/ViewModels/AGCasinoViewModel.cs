namespace Fanex.Result.Core.AGCasino.ViewModels
{
    using System.Globalization;
    using Base;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using DataDomain;
    using DTO;
    using Utilities;

    public class AGCasinoViewModel : BaseViewModel, IResultViewModel
    {
        private readonly AGCasino _casino;
        private string _htmlResult;

        public AGCasinoViewModel(AGCasino casino, ResultSetting setting)
            : this(casino, setting, null)
        {
        }

        public AGCasinoViewModel(AGCasino casino, ResultSetting setting, IRefDataHelper refDataHelper)
            : base(setting, refDataHelper)
        {
            _casino = casino;
            LanguageCode = setting.LanguageCode;
            Sport = setting.SportType;
        }

        public override string LeagueName
            => RefDataHelper.GetBetTypeNameById(
                _casino.Bettype.ToString(CultureInfo.InvariantCulture),
                GetSupportedLanguage,
                string.Empty);

        public override string EventStatus => LangResource.Completed;

        public string Result => _htmlResult ?? (_htmlResult = GetResult());

        private string GetResult()
        {
            var htmlResult = LangResource.NoInfo;

            var resultToken = AGCasinoHelper.GetAGCasinoResultToken(_casino.Result);

            if (!string.IsNullOrEmpty(resultToken))
            {
                switch (_casino.Bettype)
                {
                    case AGCasinoBettype.Bacarat:
                        htmlResult = AGCasinoHelper.GetBacaratResult(resultToken, LangResource.Banker, LangResource.Player);
                        break;

                    case AGCasinoBettype.Roulette:
                        htmlResult = AGCasinoHelper.GetRoutletResult(resultToken);
                        break;

                    case AGCasinoBettype.DragonTiger:
                        htmlResult = AGCasinoHelper.GetDragonTigerResult(resultToken, LangResource.Dragon, LangResource.Tiger);
                        break;

                    case AGCasinoBettype.SicBo:
                        htmlResult = AGCasinoHelper.GetSicBoResult(resultToken);
                        break;

                    default:
                        htmlResult = string.Empty;
                        break;
                }
            }

            return htmlResult;
        }
    }
}