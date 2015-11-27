// -----------------------------------------------------------------------
// <copyright file="AGCasinoViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.AGCasino.ViewModels
{
    using System.Globalization;
    using Fanex.Result.Core.AGCasino.DataDomain;
    using Fanex.Result.Core.AGCasino.DTO;
    using Fanex.Result.Core.AGCasino.Utilities;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.ViewModels;
    using RemotingRef;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AGCasinoViewModel : BaseViewModel, IResultViewModel
    {
        private readonly AGCasino _casino;
        private string _htmlResult;

        public AGCasinoViewModel(AGCasino casino, ResultSetting setting)
            : base(setting)
        {
            this._casino = casino;
            this.LanguageCode = setting.LanguageCode;
            this.Sport = setting.SportType;
        }

        public override string LeagueName
        {
            get
            {
                return RefData.GetBetTypeName(
               this._casino.Bettype.ToString(CultureInfo.InvariantCulture),
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

        public string Result
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

        private void BindingResult()
        {
            _htmlResult = LangResource.NoInfo;

            var resultToken = AGCasinoHelper.GetAGCasinoResultToken(_casino.Result);
            if (!string.IsNullOrEmpty(resultToken))
            {
                switch (_casino.Bettype)
                {
                    case AGCasinoBettype.Bacarat:
                        _htmlResult = AGCasinoHelper.GetBacaratResult(resultToken, LangResource.Banker, LangResource.Player);
                        break;

                    case AGCasinoBettype.Roulette:
                        _htmlResult = AGCasinoHelper.GetRoutletResult(resultToken);
                        break;

                    case AGCasinoBettype.DragonTiger:
                        _htmlResult = AGCasinoHelper.GetDragonTigerResult(resultToken, LangResource.Dragon, LangResource.Tiger);
                        break;

                    case AGCasinoBettype.SicBo:
                        _htmlResult = AGCasinoHelper.GetSicBoResult(resultToken);
                        break;
                }
            }
        }
    }
}