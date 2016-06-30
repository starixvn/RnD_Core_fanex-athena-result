namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System.Globalization;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.ViewModels;

    public sealed class BilliardViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public BilliardViewModel(DTO.Sportsbook sportbook, ResultSetting setting)
            : base(sportbook, setting)
        {
        }

        public string FinalAwayScore
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue
                    ? this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture)
                    : ConstantValue.Hyphen;
            }
        }

        public string FinalHomeScore
        {
            get
            {
                return this.SPORTBOOK.FinalHomeScore.HasValue ?
                    this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture)
                    : ConstantValue.Hyphen;
            }
        }
    }
}