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
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.ViewModels;
    using Newtonsoft.Json.Linq;
    using RemotingRef;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AGCasinoViewModel : BaseViewModel, IResultViewModel
    {
        private readonly AGCasino _agCasino;
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
            _htmlResult = Lang.NoInfo;

            if (!string.IsNullOrEmpty(_agCasino.Result))
            {
                dynamic gameResult = JObject.Parse(_agCasino.Result);
                var resultToken = gameResult.result != null ? gameResult.result.Value : string.Empty;
                if (!string.IsNullOrEmpty(resultToken))
                {
                    switch (_agCasino.Bettype)
                    {
                        case AGCasinoBettype.Bacarat:
                            _htmlResult = AGCasinoHelper.GetBacaratResult(resultToken, Lang.Banker, Lang.Player);
                            break;

                        case AGCasinoBettype.Roulette:
                            _htmlResult = AGCasinoHelper.GetRoutletResult(resultToken);
                            break;

                        case AGCasinoBettype.DragonTiger:
                            _htmlResult = AGCasinoHelper.GetDragonTigerResult(resultToken, Lang.Daragon, Lang.Tiger);
                            break;

                        case AGCasinoBettype.SicBo:
                            _htmlResult = AGCasinoHelper.GetSicBoResult(resultToken);
                            break;
                    }
                }
            }
        }
    }
}