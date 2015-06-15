// -----------------------------------------------------------------------
// <copyright file="VSTennisViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.ViewModels;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class VSTennisViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public VSTennisViewModel(DTO.Sportsbook sportbook, ResultSetting setting, LangResources resources)
            : base(sportbook, setting, resources)
        {
        }

        public string HomePoint
        {
            get
            {
                return this.SPORTBOOK.FinalHomeScore.HasValue ? this.SPORTBOOK.FinalHomeScore.Value.ToString() : ConstantValue.Hyphen;
            }
        }

        public string AwayPoint
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue ? this.SPORTBOOK.FinalAwayScore.Value.ToString() : ConstantValue.Hyphen;
            }
        }
    }
}