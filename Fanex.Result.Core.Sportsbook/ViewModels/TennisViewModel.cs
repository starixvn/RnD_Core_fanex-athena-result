namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using Base.Extension;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using DataDomain;
    using DTO;
    using TennisDetail;
    using Utilities;

    public class TennisViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private int _totalGameHome;
        private int _totalGameAway;
        private SetValue[] _setValue;
        private string _totalSetHome;
        private string _totalSetAway;

        public TennisViewModel(Sportsbook sportbook, ResultSetting setting)
            : base(sportbook, setting)
        {
        }

        public override string EventStatus
        {
            get
            {
                var matchResultInfo = SPORTBOOK.GameDetails
                    .SingleOrDefault(i => i.ResourceId.Equals(TennisBetID.TotalSet, System.StringComparison.OrdinalIgnoreCase));
                if (matchResultInfo != null && matchResultInfo.SettlementStatusID == EventStatusID.RefundSettlementStatusID)
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
                    _setValue = new[] { Set1, Set2, Set3, Set4, Set5 };
                }

                return _setValue;
            }
        }

        public override bool VisibleDetailedLink
        {
            get
            {
                return base.VisibleDetailedLink
                    && SPORTBOOK.GameDetails.Any(i => !string.IsNullOrEmpty(i.ResourceId)
                 && i.ResourceId.Trim().Length == ConstantValue.SetXGameYResourceIdLength);
            }
        }

        public override bool VisibleMatchResult
        {
            get { return SPORTBOOK.Bettype != BetTypeID.SetXGameYWinner; }
        }

        public override bool VisibleGameDetail
        {
            get
            {
                return SPORTBOOK.Bettype == BetTypeID.SetXGameYWinner && SportsbookHelper.IsSetXGameYBetID(SPORTBOOK.BetID);
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
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan",
Justification = "Reviewed. Suppression is OK here.")]
        public override string GetHomeAwaySet(int index, bool isHome)
        {
            if (index > 4)
            {
                return ConstantValue.Hyphen;
            }

            if (HomeSet == null || AwaySet == null)
            {
                _totalGameAway = 0;
                _totalGameHome = 0;
                HomeSet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen };
                AwaySet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen };

                if (SPORTBOOK.GameDetails.Count > 0)
                {
                    for (var i = 0; i < 5; i++)
                    {
                        GenerateHomeAwaySet(i);
                    }
                }
            }

            return isHome ? HomeSet[index] : AwaySet[index];
        }

        private void GenerateHomeAwaySet(int i)
        {
            var setIndex = (i + 1).ToString("D2");
            var set = SPORTBOOK.GameDetails.FirstOrDefault(a => a.ResourceId == setIndex);

            if (set != null)
            {
                switch (set.SettlementStatusID)
                {
                    case EventStatusID.CompletedSettlementStatusID:
                    case EventStatusID.RunningSettlementStatusID:
                        HomeSet[i] = set.HomeValue;
                        AwaySet[i] = set.AwayValue;

                        if (Helper.IsIntegerNumber(set.HomeValue))
                        {
                            _totalGameHome += int.Parse(set.HomeValue);
                        }

                        if (Helper.IsIntegerNumber(set.AwayValue))
                        {
                            _totalGameAway += int.Parse(set.AwayValue);
                        }

                        break;

                    case EventStatusID.RefundSettlementStatusID:
                        HomeSet[i] = LangResource.Refund;
                        AwaySet[i] = LangResource.Refund;
                        break;

                    case EventStatusID.RefundHTSettlementStatusID:
                        HomeSet[i] = LangResource.Refundht;
                        AwaySet[i] = LangResource.Refundht;
                        break;

                    case EventStatusID.RefundFTSettlementStatusID:
                        HomeSet[i] = LangResource.Refundft;
                        AwaySet[i] = LangResource.Refundft;
                        break;

                    default:
                        HomeSet[i] = ConstantValue.Hyphen;
                        AwaySet[i] = ConstantValue.Hyphen;
                        break;
                }
            }
        }

        /// <summary>
        /// Get visible status of sets.
        /// </summary>
        /// <param name="set">The set index.</param>
        /// <returns>The visible status.</returns>
        public bool VisibleSet(int set)
        {
            var betID = int.Parse(SPORTBOOK.BetID);

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
            if (HighlightWinnerTeam && Helper.IsIntegerNumber(TotalSetHome) && Helper.IsIntegerNumber(TotalSetAWay))
            {
                var score = Convert.ToInt32(TotalSetHome) - Convert.ToInt32(TotalSetAWay);
                var homeWin = isHome && score > 0;
                var awayWin = !isHome && score < 0;

                return homeWin || awayWin ? ConstantValue.WinFormat : string.Empty;
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
            if (string.IsNullOrEmpty(_totalSetHome) || string.IsNullOrEmpty(_totalSetAway))
            {
                var set = SPORTBOOK.GameDetails.FirstOrDefault(i => i.ResourceId == GameResourceID.FinalScore);

                if (set != null)
                {
                    switch (set.SettlementStatusID)
                    {
                        case EventStatusID.CompletedSettlementStatusID:
                        case EventStatusID.RunningSettlementStatusID:
                            _totalSetHome = set.HomeValue;
                            _totalSetAway = set.AwayValue;
                            break;

                        case EventStatusID.RefundSettlementStatusID:
                            _totalSetHome = _totalSetAway = LangResource.Refund;
                            break;

                        case EventStatusID.RefundHTSettlementStatusID:
                            _totalSetHome = _totalSetAway = LangResource.Refundht;
                            break;

                        case EventStatusID.RefundFTSettlementStatusID:
                            _totalSetHome = _totalSetAway = LangResource.Refundft;
                            break;

                        default:
                            _totalSetHome = _totalSetAway = ConstantValue.Hyphen;
                            break;
                    }
                }
                else
                {
                    _totalSetHome = _totalSetAway = ConstantValue.Hyphen;
                }
            }

            return isHome ? _totalSetHome : _totalSetAway;
        }

        /// <summary>
        /// Get total game value.
        /// </summary>
        /// <param name="isHome">If true, return home total game value.</param>
        /// <returns>The total game value.</returns>
        private string GetTotalGameValue(bool isHome)
        {
            var set = SPORTBOOK.GameDetails.FirstOrDefault(i => i.ResourceId == GameResourceID.FinalScore);
            if (set == null || set.SettlementStatusID != EventStatusID.CompletedSettlementStatusID)
            {
                return GetTotalSetValue(isHome);
            }

            if (HomeSet == null || AwaySet == null)
            {
                GetHomeAwaySet(0, isHome);
            }

            var totalGameValue = _totalGameHome + _totalGameAway;

            // TODO: Refactor later
#pragma warning disable S3240 // The simplest possible condition syntax should be used
            if (isHome)
#pragma warning restore S3240 // The simplest possible condition syntax should be used
            {
                return totalGameValue > 0 ? _totalGameHome.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
            }
            else
            {
                return totalGameValue > 0 ? _totalGameAway.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
            }
        }

        /// <summary>
        /// Create detail value of sets.
        /// </summary>
        /// <param name="setIndex">The set index.</param>
        /// <returns>The set value.</returns>
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan",
    Justification = "Reviewed. Suppression is OK here.")]
        private SetValue CreateSetAndGameValue(string setIndex)
        {
            var setIndexId = int.Parse(setIndex);
            var gameDetails = SPORTBOOK.GameDetails.Where(i => SportsbookHelper.IsTennisGameResourceID(setIndexId, i.ResourceId))
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

            var numOfGame = int.Parse(gameDetails.LastOrDefault().ResourceId) - (setIndexId * 100);

            if (numOfGame > ConstantValue.NumberGameOfTennisSet)
            {
                var modeValue = numOfGame % ConstantValue.NumberGameOfTennisSet;

                if (modeValue > 0)
                {
                    numOfGame += ConstantValue.NumberGameOfTennisSet - modeValue;
                }
            }
            else
            {
                if (numOfGame < ConstantValue.NumberGameOfTennisSet)
                {
                    numOfGame += ConstantValue.NumberGameOfTennisSet - numOfGame;
                }
            }

            for (var j = 1; j <= numOfGame; j++)
            {
                var resourceId = setIndex + j.ToString("D2");
                var game = gameDetails.SingleOrDefault(i => i.ResourceId == resourceId);

                result.Games.Add(game != null ? CreateGameValue(j, game) : new GameValue
                {
                    GameId = j,
                    HomeValue = ConstantValue.Hyphen,
                    AwayValue = ConstantValue.Hyphen
                });
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

                    case EventStatusID.RefundSettlementStatusID:
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