// -----------------------------------------------------------------------
// <copyright file="BilliardViewModel.cs" company="Nexcel">
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
    public sealed class BilliardViewModel : SportbookBaseViewModel
    {
        public BilliardViewModel(Sportbook sportbook, SportType sport, string languageCode, LangResources resouces)
            : base(sportbook, sport, languageCode, resouces)
        {
        }

        public string FinalAwayScore
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue
                    ? this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture)
                    : string.Empty;
            }
        }

        public string FinalHomeScore
        {
            get
            {
                return this.SPORTBOOK.FinalHomeScore.HasValue ?
                    this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture)
                    : string.Empty;
            }
        }
    }
}