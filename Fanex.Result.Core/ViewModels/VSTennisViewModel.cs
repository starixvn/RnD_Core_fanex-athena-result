// -----------------------------------------------------------------------
// <copyright file="VSTennisViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using Fanex.Result.Core.DTO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class VSTennisViewModel : SportbookBaseViewModel
    {
        public VSTennisViewModel(Sportbook sportbook, string languageCode, LangResources resources)
            : base(sportbook, languageCode, resources)
        {
        }

        public string HomePoint
        {
            get
            {
                return this.SPORTBOOK.FinalHomeScore.HasValue ? this.SPORTBOOK.FinalHomeScore.Value.ToString() : string.Empty;
            }
        }

        public string AwayPoint
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue ? this.SPORTBOOK.FinalAwayScore.Value.ToString() : string.Empty;
            }
        }
    }
}