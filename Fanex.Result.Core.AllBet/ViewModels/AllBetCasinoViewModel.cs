// -----------------------------------------------------------------------
// <copyright file="AGCasinoViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.AllBetCasino.ViewModels
{
    using DataDomain;    
    using Fanex.Result.Core.AllBetCasino.DTO;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.ViewModels;
    using RemotingRef;
    using System.Globalization;
    using Utilities;
/// <summary>
                         /// TODO: Update summary.
                         /// </summary>
    public class AllBetCasinoViewModel : BaseViewModel, IResultViewModel
    {
        private readonly AllBetCasino _casino;
        private string _htmlResult;

        public AllBetCasinoViewModel(AllBetCasino casino, ResultSetting setting)
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

            var resultToken = AllBetCasinoHelper.GetAGCasinoResultToken(_casino.Result);
            if (!string.IsNullOrEmpty(resultToken))
            {
                switch (_casino.Bettype)
                {
                    case AllBetCasinoBettype.Bacarat:
                        _htmlResult = AllBetCasinoHelper.GetBacaratResult(resultToken, LangResource.Banker, LangResource.Player);
                        break;

                    case AllBetCasinoBettype.Roulette:
                        _htmlResult = AllBetCasinoHelper.GetRoutletResult(resultToken);
                        break;

                    case AllBetCasinoBettype.DragonTiger:
                        _htmlResult = AllBetCasinoHelper.GetDragonTigerResult(resultToken, LangResource.Dragon, LangResource.Tiger);
                        break;

                    case AllBetCasinoBettype.SicBo:
                        _htmlResult = AllBetCasinoHelper.GetSicBoResult(resultToken);
                        break;
                }
            }
        }
    }
}