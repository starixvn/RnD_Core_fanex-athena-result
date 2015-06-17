// -----------------------------------------------------------------------
// <copyright file="MicroGaming.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.MicroGaming.DTO
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MicroGaming
    {
        public string SiteName { get; set; }

        public int DepositCustId { get; set; }

        public int GameId { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Reviewed!")]
        public string APIUrl { get; set; }

        public string NoInformationMessage { get; set; }
    }
}