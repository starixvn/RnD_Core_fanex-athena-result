// -----------------------------------------------------------------------
// <copyright file="SoccerViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System;
    using System.Globalization;
    using Fanex.Result.Core.DTO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class SoccerViewModel : SportbookBaseViewModel
    {
        public SoccerViewModel(Sportbook soccer, string languageCode, LangResources resource)
            : base(soccer, languageCode, resource)
        {
        }

        #region Setttlement Site

        public int HomeId
        {
            get { return this.SPORTBOOK.HomeId; }
        }

        public int AwayId
        {
            get { return this.SPORTBOOK.AwayId; }
        }

        public string GoalRecord
        {
            get { return this.SPORTBOOK.GoalRecord; }
        }

        #endregion Setttlement Site

        public bool IsShowFGHome
        {
            get
            {
                string betStatus;
                switch (this.SPORTBOOK.EventStatus)
                {
                    case StringResources.REFUND:
                    case StringResources.REFUNDHT:
                        return false;
                    case StringResources.REFUNDFT:
                        betStatus = this.SPORTBOOK.BetStatus.Trim();
                        if (!string.IsNullOrEmpty(betStatus) && betStatus.Contains(":"))
                        {
                            string[] refundArray = betStatus.Split(';');
                            if (refundArray.Length == 2 && !string.IsNullOrEmpty(refundArray[1]))
                            {
                                var fglgArray = refundArray[1].Split(':');
                                if (refundArray[1].Length <= 1 || string.Compare(fglgArray[0], "FL", StringComparison.OrdinalIgnoreCase) != 0)
                                {
                                    return false;
                                }

                                var firstGoal = fglgArray[1].Split('-')[0];
                                if (firstGoal == "1")
                                {
                                    return true;
                                }
                            }
                        }

                        return false;

                    default:
                        betStatus = this.SPORTBOOK.BetStatus.Trim();
                        if (!string.IsNullOrEmpty(betStatus) && betStatus.Contains(":"))
                        {
                            var fglgArray = betStatus.Split(':');

                            if (betStatus.Length <= 1 || string.Compare(fglgArray[0], "FL", StringComparison.OrdinalIgnoreCase) != 0)
                            {
                                return false;
                            }

                            var firstGoal = fglgArray[1].Split('-')[0];

                            if (firstGoal == "1")
                            {
                                return true;
                            }
                        }

                        return false;
                }
            }
        }

        public bool IsShowFGAway
        {
            get
            {
                string betStatus;
                switch (this.SPORTBOOK.EventStatus)
                {
                    case StringResources.REFUND:
                    case StringResources.REFUNDHT:
                        return false;
                    case StringResources.REFUNDFT:
                        betStatus = this.SPORTBOOK.BetStatus.Trim();
                        if (!string.IsNullOrEmpty(betStatus) && betStatus.Contains(":"))
                        {
                            string[] refundArray = betStatus.Split(';');
                            if (refundArray.Length == 2 && !string.IsNullOrEmpty(refundArray[1]))
                            {
                                var fglgArray = refundArray[1].Split(':');
                                if (refundArray[1].Length <= 1 || string.Compare(fglgArray[0], "FL", StringComparison.OrdinalIgnoreCase) != 0)
                                {
                                    return false;
                                }

                                var firstGoal = fglgArray[1].Split('-')[0];
                                if (firstGoal == "2")
                                {
                                    return true;
                                }
                            }
                        }

                        return false;

                    default:
                        betStatus = this.SPORTBOOK.BetStatus.Trim();
                        if (!string.IsNullOrEmpty(betStatus) && betStatus.Contains(":"))
                        {
                            var fglgArray = betStatus.Split(':');
                            if (betStatus.Length <= 1 || string.Compare(fglgArray[0], "FL", StringComparison.OrdinalIgnoreCase) != 0)
                            {
                                return false;
                            }

                            var firstGoal = fglgArray[1].Split('-')[0];

                            if (firstGoal == "2")
                            {
                                return true;
                            }
                        }

                        return false;
                }
            }
        }

        public bool IsShowLGHome
        {
            get
            {
                string betStatus;
                switch (this.SPORTBOOK.EventStatus)
                {
                    case StringResources.REFUND:
                    case StringResources.REFUNDHT:
                        return false;
                    case StringResources.REFUNDFT:
                        betStatus = this.SPORTBOOK.BetStatus.Trim();
                        if (!string.IsNullOrEmpty(betStatus) && betStatus.Contains(":"))
                        {
                            string[] refundArray = betStatus.Split(';');
                            if (refundArray.Length == 2 && !string.IsNullOrEmpty(refundArray[1]))
                            {
                                var fglgArray = refundArray[1].Split(':');
                                if (refundArray[1].Length <= 1 || string.Compare(fglgArray[0], "FL", StringComparison.OrdinalIgnoreCase) != 0)
                                {
                                    return false;
                                }

                                var lastGoal = fglgArray[1].Split('-')[1];
                                if (lastGoal == "1")
                                {
                                    return true;
                                }
                            }
                        }

                        return false;

                    default:
                        betStatus = this.SPORTBOOK.BetStatus.Trim();
                        if (!string.IsNullOrEmpty(betStatus) && betStatus.Contains(":"))
                        {
                            var fglgArray = betStatus.Split(':');
                            if (betStatus.Length <= 1 || string.Compare(fglgArray[0], "FL", StringComparison.OrdinalIgnoreCase) != 0)
                            {
                                return false;
                            }

                            var lastGoal = fglgArray[1].Split('-')[1];

                            if (lastGoal == "1")
                            {
                                return true;
                            }
                        }

                        return false;
                }
            }
        }

        public bool IsShowLGAway
        {
            get
            {
                string betStatus;
                switch (this.SPORTBOOK.EventStatus)
                {
                    case StringResources.REFUND:
                    case StringResources.REFUNDHT:
                        return false;
                    case StringResources.REFUNDFT:
                        betStatus = this.SPORTBOOK.BetStatus.Trim();
                        if (!string.IsNullOrEmpty(betStatus) && betStatus.Contains(":"))
                        {
                            string[] refundArray = betStatus.Split(';');
                            if (refundArray.Length == 2 && !string.IsNullOrEmpty(refundArray[1]))
                            {
                                var fglgArray = refundArray[1].Split(':');
                                if (refundArray[1].Length <= 1 || string.Compare(fglgArray[0], "FL", StringComparison.OrdinalIgnoreCase) != 0)
                                {
                                    return false;
                                }

                                var lastGoal = fglgArray[1].Split('-')[1];
                                if (lastGoal == "2")
                                {
                                    return true;
                                }
                            }
                        }

                        return false;

                    default:
                        betStatus = this.SPORTBOOK.BetStatus.Trim();
                        if (!string.IsNullOrEmpty(betStatus) && betStatus.Contains(":"))
                        {
                            var fglgArray = betStatus.Split(':');
                            if (betStatus.Length <= 1 || string.Compare(fglgArray[0], "FL", StringComparison.OrdinalIgnoreCase) != 0)
                            {
                                return false;
                            }

                            var lastGoal = fglgArray[1].Split('-')[1];

                            if (lastGoal == "2")
                            {
                                return true;
                            }
                        }

                        return false;
                }
            }
        }

        public string HTHomeScore
        {
            get
            {
                if (this.SPORTBOOK.EventStatus == StringResources.REFUND
                    || this.SPORTBOOK.EventStatus == StringResources.REFUNDHT || this.SPORTBOOK.EventStatus == StringResources.Postponed)
                {
                    return StringResources.NA;
                }

                return this.SPORTBOOK.HtHomeScore.HasValue ? this.SPORTBOOK.HtHomeScore.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
            }
        }

        public string FinalHomeScore
        {
            get
            {
                if (this.SPORTBOOK.EventStatus == StringResources.REFUND || this.SPORTBOOK.EventStatus == StringResources.REFUNDFT
                    || this.SPORTBOOK.EventStatus == StringResources.Postponed)
                {
                    return StringResources.NA;
                }

                return this.SPORTBOOK.FinalHomeScore.HasValue ? this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
            }
        }

        public string HTAwayScore
        {
            get
            {
                if (this.SPORTBOOK.EventStatus == StringResources.REFUND
                    || this.SPORTBOOK.EventStatus == StringResources.REFUNDHT || this.SPORTBOOK.EventStatus == StringResources.Postponed)
                {
                    return StringResources.NA;
                }

                return this.SPORTBOOK.HtAwayScore.HasValue ? this.SPORTBOOK.HtAwayScore.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
            }
        }

        public string FinalAwayScore
        {
            get
            {
                if (this.SPORTBOOK.EventStatus == StringResources.REFUND
                    || this.SPORTBOOK.EventStatus == StringResources.REFUNDFT || this.SPORTBOOK.EventStatus == StringResources.Postponed)
                {
                    return StringResources.NA;
                }

                return this.SPORTBOOK.FinalAwayScore.HasValue ? this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
            }
        }

        public bool IsShowEventStatusLink
        {
            // only used for settlement site
            get
            {
                if (this.SPORTBOOK.EventStatus.ToLower() == StringResources.CompletedLower)
                {
                    if (!string.IsNullOrEmpty(this.SPORTBOOK.GoalRecord) && this.SPORTBOOK.BetStatus.IndexOf("FL:", StringComparison.Ordinal) > -1)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}