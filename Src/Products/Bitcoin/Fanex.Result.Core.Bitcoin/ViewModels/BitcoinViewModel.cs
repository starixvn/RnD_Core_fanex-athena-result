namespace Fanex.Result.Core.Bitcoin.ViewModels
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;

    public sealed class BitcoinViewModel : BaseViewModel, IResultViewModel
    {
        private readonly DTO.Bitcoin bitcoin;

        public BitcoinViewModel(DTO.Bitcoin bitcoin, ResultSetting setting)
            : this(bitcoin, setting, null)
        {
        }

        public BitcoinViewModel(DTO.Bitcoin bitcoin, ResultSetting setting, IRefDataHelper refDataHelper)
           : base(setting, refDataHelper)
        {
            this.bitcoin = bitcoin;
            LanguageCode = setting.LanguageCode;
        }

        public override string KickOffTime => GetFormatedKickOffTime(bitcoin.EventDate);

        public override int LeagueId => bitcoin.League3rdId;

        public override string LeagueName => RefDataHelper.GetLeague3rdName(LeagueId, GetSupportedLanguage, string.Empty);

        public string MatchId => bitcoin.Match3rdId.ToString(CultureInfo.InvariantCulture);

        public string ResultPrice
        {
            get
            {
                var resultPrice = bitcoin.GameResults.FirstOrDefault();
                if (resultPrice != null)
                {
                    return string.IsNullOrEmpty(resultPrice.GameResult) ?
                        ConstantValue.Hyphen :
                        Helper.DecFormat(Decimal.Parse(resultPrice.GameResult), 2);
                }

                return ConstantValue.Hyphen;
            }
        }

        public override string EventStatus
        {
            get
            {
                if (bitcoin.EventStatus.Equals(ConstantValue.Completed, StringComparison.OrdinalIgnoreCase)
                && string.IsNullOrEmpty(bitcoin.BetStatus))
                {
                    return LangResource.Completed;
                }
                else if (bitcoin.EventStatus.Equals(ConstantValue.Completed, StringComparison.OrdinalIgnoreCase)
                    && bitcoin.BetStatus.Equals(ConstantValue.Refund, StringComparison.OrdinalIgnoreCase))
                {
                    return LangResource.Refund;
                }

                return ConstantValue.Hyphen;
            }
        }

    }
}