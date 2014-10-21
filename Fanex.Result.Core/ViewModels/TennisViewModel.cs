// -----------------------------------------------------------------------
// <copyright file="TennisViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.ViewModels.TennisDetail;

    /// <summary>
    /// Tennis view model class.
    /// </summary>
    public class TennisViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private int _totalGameHome;
        private int _totalGameAway;
        private bool _hasRefundedSet;

        public TennisViewModel(Sportsbook sportbook, ResultSetting setting, LangResources resources)
            : base(sportbook, setting, resources)
        {
        }

        public string TotalGameHome
        {
            get
            {
                if (this._hasRefundedSet)
                {
                    return this.TotalSetHome;
                }

                return this._totalGameHome > 0 ? this._totalGameHome.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN;
            }
        }

        public string TotalGameAway
        {
            get
            {
                if (this._hasRefundedSet)
                {
                    return this.TotalSetAWay;
                }

                return this._totalGameAway > 0 ? this._totalGameAway.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN;
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
                return new SetValue[5] { this.Set1, this.Set2, this.Set3, this.Set4, this.Set5 };
            }
        }

        public override bool VisibleDetailedLink
        {
            get
            {
                return base.VisibleDetailedLink && this.SPORTBOOK.GameDetails.Count > 0
                    && this.SPORTBOOK.GameDetails.Any(i => !string.IsNullOrEmpty(i.ResourceId)
                        && i.ResourceId.Trim().Length == ConstantValueDefinition.SETXGAMEYRESOURCEIDLENGTH);
            }
        }

        private SetValue Set1
        {
            get
            {
                return CreateSetValue("01");
            }
        }

        private SetValue Set2
        {
            get { return CreateSetValue("02"); }
        }

        private SetValue Set3
        {
            get { return CreateSetValue("03"); }
        }

        private SetValue Set4
        {
            get { return CreateSetValue("04"); }
        }

        private SetValue Set5
        {
            get { return CreateSetValue("05"); }
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
                return StringResources.HYPHEN;
            }

            if (this.HomeSet == null || this.AwaySet == null)
            {
                this._totalGameAway = 0;
                this._totalGameHome = 0;
                this.HomeSet = new[] { StringResources.HYPHEN, StringResources.HYPHEN, StringResources.HYPHEN, StringResources.HYPHEN, StringResources.HYPHEN };
                this.AwaySet = new[] { StringResources.HYPHEN, StringResources.HYPHEN, StringResources.HYPHEN, StringResources.HYPHEN, StringResources.HYPHEN };
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
                                case EventStatusIDDefinition.CompletedSettlementStatusID:
                                    this.HomeSet[i] = set.HomeValue;
                                    this.AwaySet[i] = set.AwayValue;
                                    this._totalGameHome += int.Parse(set.HomeValue);
                                    this._totalGameAway += int.Parse(set.AwayValue);
                                    break;

                                case EventStatusIDDefinition.RefundStettlementStatusID:
                                    this.HomeSet[i] = Lang.Refund;
                                    this.AwaySet[i] = Lang.Refund;
                                    this._hasRefundedSet = true;
                                    break;

                                case EventStatusIDDefinition.RefundHTSettlementStatusID:
                                    this.HomeSet[i] = Lang.Refundht;
                                    this.AwaySet[i] = Lang.Refundht;
                                    this._hasRefundedSet = true;
                                    break;

                                case EventStatusIDDefinition.RefundFTSettlementStatusID:
                                    this.HomeSet[i] = Lang.Refundft;
                                    this.AwaySet[i] = Lang.Refundft;
                                    this._hasRefundedSet = true;
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
        /// <param name="betID">The bet ID.</param>
        /// <returns>The visible status.</returns>
        public bool VisibleSet(int set, int betID)
        {
            switch (set)
            {
                case 1:
                    return betID >= BetIDDefinition.MinSet1GameY && betID <= BetIDDefinition.MaxSet1GameY;
                case 2:
                    return betID >= BetIDDefinition.MinSet2GameY && betID <= BetIDDefinition.MaxSet2GameY;
                case 3:
                    return betID >= BetIDDefinition.MinSet3GameY && betID <= BetIDDefinition.MaxSet3GameY;
                case 4:
                    return betID >= BetIDDefinition.MinSet4GameY && betID <= BetIDDefinition.MaxSet4GameY;

                case 5:
                    return betID >= BetIDDefinition.MinSet5GameY;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Get total set value.
        /// </summary>
        /// <param name="isHome">True if it's home set.</param>
        /// <returns>The total set value.</returns>
        private string GetTotalSetValue(bool isHome)
        {
            var set = this.SPORTBOOK.GameDetails.FirstOrDefault(i => i.ResourceId == "00");
            if (set != null)
            {
                switch (set.SettlementStatusID)
                {
                    case EventStatusIDDefinition.CompletedSettlementStatusID:
                        return isHome ? set.HomeValue : set.AwayValue;

                    case EventStatusIDDefinition.RefundStettlementStatusID:
                        return Lang.Refund;

                    case EventStatusIDDefinition.RefundHTSettlementStatusID:
                        return Lang.Refundht;

                    case EventStatusIDDefinition.RefundFTSettlementStatusID:
                        return Lang.Refundft;

                    default:
                        return StringResources.HYPHEN;
                }
            }

            return StringResources.HYPHEN;
        }

        /// <summary>
        /// Create detail value of sets.
        /// </summary>
        /// <param name="setIndex">The set index.</param>
        /// <returns>The set value.</returns>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan",
    Justification = "Reviewed. Suppression is OK here.")]
        private SetValue CreateSetValue(string setIndex)
        {
            var set = this.SPORTBOOK.GameDetails.Where(i => i.ResourceId == setIndex).FirstOrDefault();
            if (set != null)
            {
                var resources = new List<string>();
                for (var i = 1; i <= ConstantValueDefinition.NUMBERGAMEOFTENNISSET; i++)
                {
                    resources.Add(setIndex + i.ToString("D2"));
                }

                var result = new SetValue
                {
                    SetId = int.Parse(setIndex),
                    Games = new List<GameValue>()
                };
                result.IsRefund = set.ResultStatusID == EventStatusIDDefinition.RefundEventStatusID;
                if (!result.IsRefund)
                {
                    var gameDetails = this.SPORTBOOK.GameDetails.Where(i => resources.Contains(i.ResourceId)).OrderBy(i => i.ResourceId).ToArray();
                    var numofGame = gameDetails.Length < ConstantValueDefinition.NUMBERGAMEOFTENNISSET ? ConstantValueDefinition.NUMBERGAMEOFTENNISSET : gameDetails.Length;
                    for (var j = 0; j < numofGame; j++)
                    {
                        var game = gameDetails[j];
                        result.Games.Add(new GameValue
                        {
                            GameId = j + 1,
                            HomeValue = game != null && game.SettlementStatusID == EventStatusIDDefinition.CompletedSettlementStatusID ? game.HomeValue : StringResources.HYPHEN,
                            AwayValue = game != null && game.SettlementStatusID == EventStatusIDDefinition.CompletedSettlementStatusID ? game.AwayValue : StringResources.HYPHEN
                        });
                    }

                    var modeValue = numofGame % ConstantValueDefinition.NUMBERGAMEOFTENNISSET;

                    if (numofGame > 13 && modeValue > 0)
                    {
                        var moreGame = ConstantValueDefinition.NUMBERGAMEOFTENNISSET - modeValue + numofGame;
                        for (var k = numofGame + 1; k <= moreGame; k++)
                        {
                            result.Games.Add(new GameValue
                            {
                                GameId = k,
                                HomeValue = StringResources.HYPHEN,
                                AwayValue = StringResources.HYPHEN
                            });
                        }
                    }
                }

                return result;
            }

            return null;
        }
    }
}