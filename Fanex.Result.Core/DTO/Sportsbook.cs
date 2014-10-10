// -----------------------------------------------------------------------
// <copyright file="Sportsbook.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.DTO
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class Sportsbook
    {
        public string BetStatus { get; set; }

        public int LeagueId { get; set; }

        public int MatchId { get; set; }

        public string Color { get; set; }

        public string MatchCode { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime KickOffTime { get; set; }

        public int HomeId { get; set; }

        public int AwayId { get; set; }

        public int? HtHomeScore { get; set; }

        public int? HtAwayScore { get; set; }

        public int? FinalHomeScore { get; set; }

        public int? FinalAwayScore { get; set; }

        public string EventStatus { get; set; }

        public bool IsPending { get; set; }

        public bool IsNeutral { get; set; }

        public string GoalRecord { get; set; }

        public bool Calculate { get; set; }

        public bool IsHTCal { get; set; }

        /// <summary>
        /// Gets or sets the result value.
        /// Only use for PopUp Result and sport type is Tennis.
        /// To show more match detail (sporting solution).
        /// Example :6-1 *30-40 40-30* *40-0 40-0* *40-0 40-0* *40-0.
        ///  #pragma warning disable 1570
        /// Read more info at:
        /// <![CDATA[http://ccs.nexcel.vn/viewtopic.php?f=206&t=7240&p=33960#p33960]]>
        /// </summary>
        /// <value>Get from Result column.</value>
        public string ResultValue { get; set; }

        /// <summary>
        /// Gets or sets RefNo.It's used for popUp Result.
        /// </summary>
        /// <value>Get from RefNo column.</value>
        public string RefNo { get; set; }
    }
}