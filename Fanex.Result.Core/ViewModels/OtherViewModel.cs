// -----------------------------------------------------------------------
// <copyright file="OtherViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System.Globalization;
    using Fanex.Result.Core.DTO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class OtherViewModel : SportbookBaseViewModel
    {
        public OtherViewModel(Sportbook sportbook, SportType sport, string languageCode, LangResources resouces)
            : base(sportbook, sport, languageCode, resouces)
        {
        }

        public string HTHomeScore
        {
            get
            {
                return this.SPORTBOOK.HtHomeScore.HasValue ? this.SPORTBOOK.HtHomeScore.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
            }
        }

        public string HTAwayScore
        {
            get
            {
                return this.SPORTBOOK.HtAwayScore.HasValue ? this.SPORTBOOK.HtAwayScore.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
            }
        }

        public string FinalAwayScore
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue ?
                    this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture)
                    : string.Empty;
            }
        }

        public string FinalHomeScore
        {
            get
            {
                return this.SPORTBOOK.FinalHomeScore.HasValue
                    ? this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture)
                    : string.Empty;
            }
        }
    }
}