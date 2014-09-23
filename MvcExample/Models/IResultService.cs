// -----------------------------------------------------------------------
// <copyright file="IResultService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MvcExample.Models
{
    using System.Collections.Generic;
    using Fanex.Result.Core.DTO;
    using MvcExample.Models.Criteria;
    using MvcExample.Models.DTO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IResultService
    {
        #region normal-report

        IEnumerable<Sport> GetSports(GetSportByDateCriteria criteria);

        IEnumerable<League> GetLeagues(GetLeaguesBySportCriteria criteria);

        IEnumerable<Racing> GetRacingResult(GetRacingResultCriteria criteria);

        IEnumerable<NumberGame> GetNumberGameResult(GetNumberGameResultCriteria criteria);

        IEnumerable<LiveCasino> GetLiveCasinoResult(GetLiveCasinoResultCriteria criteria);

        IEnumerable<Sportbook> GetSportbookResult(GetSportbookResultCriteria criteria);

        #endregion normal-report

        #region PopUp

        IEnumerable<Racing> GetRacingPopupResult(GetRacingPopupResultCriteria criteria);

        IEnumerable<NumberGame> GetNumberGamePopupResult(GetNumberGamePopupResultCriteria criteria);

        IEnumerable<LiveCasino> GetLiveCasinoPopupResult(GetLiveCasinoPopupResultCriteria criteria);

        IEnumerable<Sportbook> GetSportbookPopupResult(GetSportbookPopupResultCriteria criteria);

        #endregion PopUp
        #region Parlay
        IEnumerable<Parlay> GetParlayResult(GetParlayResultCriteria criteria);

        #endregion
        #region OutRight
        IEnumerable<OutRight> GetOutRightResult(GetOutRightResult criteria);
        #endregion
    }
}