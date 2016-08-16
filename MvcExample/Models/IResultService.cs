// -----------------------------------------------------------------------
// <copyright file="IResultService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MvcExample.Models
{
    using System.Collections.Generic;
    using Fanex.Result.Core.AGCasino.DTO;
    using Fanex.Result.Core.GoldDeluxe.DTO;
    using Fanex.Result.Core.LiveCasino.DTO;
    using Fanex.Result.Core.MetricGaming.DTO;
    using Fanex.Result.Core.NumberGame.DTO;
    using Fanex.Result.Core.Racing.DTO;
    using Fanex.Result.Core.Sportsbook.DTO;
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

        IEnumerable<Sportsbook> GetSportbookResult(GetSportbookResultCriteria criteria);

        Sportsbook GetGameDetail(GetGameDetailCriteria criteria);

        #endregion normal-report

        #region PopUp

        IEnumerable<Racing> GetRacingPopupResult(GetRacingPopupResultCriteria criteria);

        IEnumerable<NumberGame> GetNumberGamePopupResult(GetNumberGamePopupResultCriteria criteria);

        IEnumerable<LiveCasino> GetLiveCasinoPopupResult(GetLiveCasinoPopupResultCriteria criteria);

        IEnumerable<Sportsbook> GetSportbookPopupResult(GetSportbookPopupResultCriteria criteria);

        IEnumerable<GoldDeluxe> GetGoldDeluxePopUpResult(GetGolDeluxePopUpResultCriteria criteria);

        IEnumerable<AGCasino> GetAGCasinoPopUpResult(GetAGCasinoPopUpResultCriteria criteria);

        string GetAllBetTransDesc(int betType);

        #endregion PopUp

        #region Parlay

        IEnumerable<Parlay> GetParlayResult(GetParlayResultCriteria criteria);

        #endregion Parlay

        #region OutRight

        IEnumerable<Outright> GetOutRightResult(GetOutRightResult criteria);

        #endregion OutRight

        #region Result3rd

        IEnumerable<MetricGaming> Get3rdResult(GetMatchResultDetail3rdSelCriteria criteria);

        #endregion Result3rd
    }
}