// -----------------------------------------------------------------------
// <copyright file="TennisViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System.Collections.Generic;
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

        public TennisViewModel(Sportsbook sportbook, ResultSetting setting, LangResources resources)
            : base(sportbook, setting, resources)
        {
            this.GetHomeAwaySet(0, true);
        }

        private SetValue Set1
        {
            get
            {
                var set = this.SPORTBOOK.GameDetails.Where(i => i.ResourceId == "01").FirstOrDefault();
                if (set == null)
                {
                    return null;
                }

                var resources = new List<string>();
                for (var i = 1; i <= ConstantValueDefinition.NUMBERGAMEOFTENNISSET; i++)
                {
                    resources.Add("01" + (i < 10 ? "0" + i.ToString() : i.ToString()));
                }
                var result = new SetValue
                {
                    SetId = 1,
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
        }

        private SetValue Set2
        {
            get { return new SetValue(); }
        }

        private SetValue Set3
        {
            get { return new SetValue(); }
        }

        private SetValue Set4
        {
            get { return new SetValue(); }
        }

        private SetValue Set5
        {
            get { return new SetValue(); }
        }

        public string TotalGameHome
        {
            get { return this._totalGameHome > 0 ? this._totalGameHome.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN; }
        }

        public string TotalGameAway
        {
            get { return this._totalGameAway > 0 ? this._totalGameAway.ToString(CultureInfo.InvariantCulture) : StringResources.HYPHEN; }
        }

        public string TotalSetHome
        {
            get
            {
                return this.SPORTBOOK.FinalHomeScore.HasValue ?
                    this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture)
                    : StringResources.HYPHEN;
            }
        }

        public string TotalSetAWay
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue
                    ? this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture)
                    : StringResources.HYPHEN;
            }
        }

        /// <summary>
        /// Get home and away set.
        /// Only used for some sport type such as tennis, badminton....
        /// </summary>
        /// <param name="index">The index of home or away array.</param>
        /// <param name="isHome">True if you want to get value of home set.</param>
        /// <returns>The string value.</returns>
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

                }
            }

            return isHome ? HomeSet[index] : AwaySet[index];
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
                return base.VisibleDetailedLink && this.SPORTBOOK.GameDetails != null
                    && this.SPORTBOOK.GameDetails.Any(i => !string.IsNullOrEmpty(i.ResourceId)
                        && i.ResourceId.Trim().Length == ConstantValueDefinition.SETXGAMEYRESOURCEIDLENGTH);
            }
        }

        public bool ShowSet(int set, int betID)
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
    }
}