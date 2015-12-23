// -----------------------------------------------------------------------
// <copyright file="TennisViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.DataDomain;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Core.Sportsbook.Utilities;
    using Fanex.Result.Core.Sportsbook.ViewModels.TennisDetail;

    /// <summary>
    /// Tennis view model class.
    /// </summary>
    public class TennisViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private int _totalGameHome;
        private int _totalGameAway;
        private SetValue[] _setValue = null;
        private string _totalSetHome;
        private string _totalSetAway;

        public TennisViewModel(DTO.Sportsbook sportbook, ResultSetting setting)
            : base(sportbook, setting)
        {
        }

        public override string EventStatus
        {
            get
            {
                var matchResultInfo = this.SPORTBOOK.GameDetails
                    .SingleOrDefault(i => i.ResourceId.Equals(TennisBetID.TotalSet, System.StringComparison.OrdinalIgnoreCase));
                if (matchResultInfo != null && matchResultInfo.SettlementStatusID == EventStatusID.RefundStettlementStatusID)
                {
                    return LangResource.Refund;
                }

                return base.EventStatus;
            }
        }

        public string TotalGameHome
        {
            get
            {
                return GetTotalGameValue(true);
            }
        }

        public string TotalGameAway
        {
            get
            {
                return GetTotalGameValue(false);
            }
        }

        public string TotalSetHome
        {
            get
            {
                return GetTotalSetValue(true);
            }
        }

        public string TotalSetAWay
        {
            get
            {
                return GetTotalSetValue(false);
            }
        }

        public SetValue[] SetDetail
        {
            get
            {
                if (_setValue == null)
                {
                    _setValue = new SetValue[5] { this.Set1, this.Set2, this.Set3, this.Set4, this.Set5 };
                }

                return _setValue;
            }
        }

        public override bool VisibleDetailedLink
        {
            get
            {
                return base.VisibleDetailedLink
                    && this.SPORTBOOK.GameDetails.Any(i => !string.IsNullOrEmpty(i.ResourceId)
                 && i.ResourceId.Trim().Length == ConstantValue.SETXGAMEYRESOURCEIDLENGTH);
            }
        }

        public override bool VisibleMatchResult
        {
            get { return this.SPORTBOOK.Bettype != BetTypeID.SetXGameYWinner; }
        }

        public override bool VisibleGameDetail
        {
            get
            {
                return this.SPORTBOOK.Bettype == BetTypeID.SetXGameYWinner && SportsbookHelper.IsSetXGameYBetID(this.SPORTBOOK.BetID);
            }
        }

        private SetValue Set1
        {
            get
            {
                return CreateSetAndGameValue("01");
            }
        }

        private SetValue Set2
        {
            get { return CreateSetAndGameValue("02"); }
        }

        private SetValue Set3
        {
            get { return CreateSetAndGameValue("03"); }
        }

        private SetValue Set4
        {
            get { return CreateSetAndGameValue("04"); }
        }

        private SetValue Set5
        {
            get { return CreateSetAndGameValue("05"); }
        }

        /// <summary>
        /// Get home and away set.
        /// Only used for some sport type such as tennis, badminton....
        /// </summary>
        /// <param name="index">The index of home or away array.</param>
        /// <param name="isHome">True if you want to get value of home set.</param>
        /// <returns>The string value.</returns>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan",
Justification = "Reviewed. Suppression is OK here.")]
        public override string GetHomeAwaySet(int index, bool isHome)
        {
            if (index > 4)
            {
                return ConstantValue.Hyphen;
            }

            if (this.HomeSet == null || this.AwaySet == null)
            {
                this._totalGameAway = 0;
                this._totalGameHome = 0;
                this.HomeSet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen };
                this.AwaySet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen };
                if (this.SPORTBOOK.GameDetails.Count > 0)
                {
                    for (var i = 0; i < 5; i++)
                    {
                        var setIndex = (i + 1).ToString("D2");
                        var set = this.SPORTBOOK.GameDetails.FirstOrDefault(a => a.ResourceId == setIndex);
                        if (set != null)
                        {
                            switch (set.SettlementStatusID)
                            {
                                case EventStatusID.CompletedSettlementStatusID:
                                case EventStatusID.RunningSettlementStatusID:
                                    this.HomeSet[i] = set.HomeValue;
                                    this.AwaySet[i] = set.AwayValue;
                                    if (Helper.IsIntegerNumber(set.HomeValue))
                                    {
                                        this._totalGameHome += int.Parse(set.HomeValue);
                                    }

                                    if (Helper.IsIntegerNumber(set.AwayValue))
                                    {
                                        this._totalGameAway += int.Parse(set.AwayValue);
                                    }

                                    break;

                                case EventStatusID.RefundStettlementStatusID:
                                    this.HomeSet[i] = LangResource.Refund;
                                    this.AwaySet[i] = LangResource.Refund;
                                    break;

                                case EventStatusID.RefundHTSettlementStatusID:
                                    this.HomeSet[i] = LangResource.Refundht;
                                    this.AwaySet[i] = LangResource.Refundht;
                                    break;

                                case EventStatusID.RefundFTSettlementStatusID:
                                    this.HomeSet[i] = LangResource.Refundft;
                                    this.AwaySet[i] = LangResource.Refundft;
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            return isHome ? HomeSet[index] : AwaySet[index];
        }

        /// <summary>
        /// Get visible status of sets.
        /// </summary>
        /// <param name="set">The set index.</param>
        /// <returns>The visible status.</returns>
        public bool VisibleSet(int set)
        {
            var betID = int.Parse(this.SPORTBOOK.BetID);

            switch (set)
            {
                case 1:
                    return betID >= TennisBetID.MinSet1GameY && betID <= TennisBetID.MaxSet1GameY;
                case 2:
                    return betID >= TennisBetID.MinSet2GameY && betID <= TennisBetID.MaxSet2GameY;
                case 3:
                    return betID >= TennisBetID.MinSet3GameY && betID <= TennisBetID.MaxSet3GameY;
                case 4:
                    return betID >= TennisBetID.MinSet4GameY && betID <= TennisBetID.MaxSet4GameY;

                case 5:
                    return betID >= TennisBetID.MinSet5GameY;

                default:
                    return false;
            }
        }

        protected override string GetWonTeam(bool isHome)
        {
            if (this.HighlightWinnerTeam && Helper.IsIntegerNumber(this.TotalSetHome) && Helper.IsIntegerNumber(this.TotalSetAWay))
            {
                var temp = Convert.ToInt32(this.TotalSetHome) - Convert.ToInt32(this.TotalSetAWay);

                return (isHome && temp > 0) || (!isHome && temp < 0) ? ConstantValue.WinFormat : string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// Get total set value.
        /// </summary>
        /// <param name="isHome">True if it's home set.</param>
        /// <returns>The total set value.</returns>
        private string GetTotalSetValue(bool isHome)
        {
            if (string.IsNullOrEmpty(this._totalSetHome) || string.IsNullOrEmpty(this._totalSetAway))
            {
                var set = this.SPORTBOOK.GameDetails.FirstOrDefault(i => i.ResourceId == GameResourceID.FinalScore);

                if (set != null)
                {
                    switch (set.SettlementStatusID)
                    {
                        case EventStatusID.CompletedSettlementStatusID:
                        case EventStatusID.RunningSettlementStatusID:
                            this._totalSetHome = set.HomeValue;
                            this._totalSetAway = set.AwayValue;
                            break;

                        case EventStatusID.RefundStettlementStatusID:
                            this._totalSetHome = this._totalSetAway = LangResource.Refund;
                            break;

                        case EventStatusID.RefundHTSettlementStatusID:
                            this._totalSetHome = this._totalSetAway = LangResource.Refundht;
                            break;

                        case EventStatusID.RefundFTSettlementStatusID:
                            this._totalSetHome = this._totalSetAway = LangResource.Refundft;
                            break;

                        default:
                            this._totalSetHome = this._totalSetAway = ConstantValue.Hyphen;
                            break;
                    }
                }
                else
                {
                    this._totalSetHome = this._totalSetAway = ConstantValue.Hyphen;
                }
            }

            return isHome ? this._totalSetHome : this._totalSetAway;
        }

        /// <summary>
        /// Get total game value.
        /// </summary>
        /// <param name="isHome">If true, return home total game value.</param>
        /// <returns>The total game value.</returns>
        private string GetTotalGameValue(bool isHome)
        {
            var set = this.SPORTBOOK.GameDetails.FirstOrDefault(i => i.ResourceId == GameResourceID.FinalScore);
            if (set == null || set.SettlementStatusID != EventStatusID.CompletedSettlementStatusID)
            {
                return this.GetTotalSetValue(isHome);
            }

            if (this.HomeSet == null || this.AwaySet == null)
            {
                this.GetHomeAwaySet(0, isHome);
            }

            var totalGameValue = this._totalGameHome + this._totalGameAway;
            if (isHome)
            {
                return totalGameValue > 0 ? this._totalGameHome.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
            }
            else
            {
                return totalGameValue > 0 ? this._totalGameAway.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
            }
        }

        /// <summary>
        /// Create detail value of sets.
        /// </summary>
        /// <param name="setIndex">The set index.</param>
        /// <returns>The set value.</returns>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan",
    Justification = "Reviewed. Suppression is OK here.")]
        private SetValue CreateSetAndGameValue(string setIndex)
        {
            var setIndexId = int.Parse(setIndex);
            var gameDetails = this.SPORTBOOK.GameDetails.Where(i => SportsbookHelper.IsTennisGameResourceID(setIndexId, i.ResourceId))
                .OrderBy(i => i.ResourceId).ToSafeList();
            if (gameDetails.Count == 0)
            {
                return null;
            }

            var result = new SetValue
            {
                SetId = setIndexId,
                Games = new List<GameValue>(),
            };

            var numofGame = int.Parse(gameDetails.LastOrDefault().ResourceId) - (setIndexId * 100);

            if (numofGame > ConstantValue.NUMBERGAMEOFTENNISSET)
            {
                var modeValue = numofGame % ConstantValue.NUMBERGAMEOFTENNISSET;

                if (modeValue > 0)
                {
                    numofGame += ConstantValue.NUMBERGAMEOFTENNISSET - modeValue;
                }
            }
            else if (numofGame < ConstantValue.NUMBERGAMEOFTENNISSET)
            {
                numofGame += ConstantValue.NUMBERGAMEOFTENNISSET - numofGame;
            }

            for (var j = 1; j <= numofGame; j++)
            {
                var resourceId = setIndex + j.ToString("D2");
                var game = gameDetails.SingleOrDefault(i => i.ResourceId == resourceId);
                if (game != null)
                {
                    result.Games.Add(CreateGameValue(j, game));
                }
                else
                {
                    result.Games.Add(new GameValue
                    {
                        GameId = j,
                        HomeValue = ConstantValue.Hyphen,
                        AwayValue = ConstantValue.Hyphen
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// Create game value.
        /// </summary>
        /// <param name="gameIndex">The game index.</param>
        /// <param name="game">The game detail value.</param>
        /// <returns>The game value.</returns>
        private GameValue CreateGameValue(int gameIndex, GameDetail game)
        {
            var item = new GameValue
            {
                GameId = gameIndex,
            };

            if (game != null)
            {
                switch (game.SettlementStatusID)
                {
                    case EventStatusID.CompletedSettlementStatusID:
                    case EventStatusID.RunningSettlementStatusID:
                        item.HomeValue = game.HomeValue;
                        item.AwayValue = game.AwayValue;
                        break;

                    case EventStatusID.RefundStettlementStatusID:
                        item.HomeValue = item.AwayValue = LangResource.Refund;
                        break;

                    case EventStatusID.RefundHTSettlementStatusID:
                        item.HomeValue = item.AwayValue = LangResource.Refundht;
                        break;

                    case EventStatusID.RefundFTSettlementStatusID:
                        item.HomeValue = item.AwayValue = LangResource.Refundft;
                        break;

                    default:
                        item.HomeValue = item.AwayValue = ConstantValue.Hyphen;
                        break;
                }
            }
            else
            {
                item.HomeValue = item.AwayValue = ConstantValue.Hyphen;
            }

            return item;
        }
    }
}