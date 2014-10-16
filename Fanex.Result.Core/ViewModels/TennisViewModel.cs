// -----------------------------------------------------------------------
// <copyright file="TennisViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System;
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
                var set = this.SPORTBOOK.GameDetail.Where(i => i.ResourceId == "01").FirstOrDefault();
                if (set == null)
                {
                    return null;
                }

                var resources = new List<string>();
                for (var i = 1; i <= ConstantValue.NUMBERGAMEOFTENNISSET; i++)
                {
                    resources.Add("01" + (i < 10 ? "0" + i.ToString() : i.ToString()));
                }
                var result = new SetValue
                {
                    SetId = 1,
                    Games = new List<GameValue>()
                };
                result.IsRefund = set.ResultStatusID == EventStatusID.RefundEventStatusID;
                if (!result.IsRefund)
                {
                    var gameDetails = this.SPORTBOOK.GameDetail.Where(i => resources.Contains(i.ResourceId)).OrderBy(i => i.ResourceId).ToArray();
                    var numofGame = gameDetails.Length < ConstantValue.NUMBERGAMEOFTENNISSET ? ConstantValue.NUMBERGAMEOFTENNISSET : gameDetails.Length;
                    for (var j = 0; j < numofGame; j++)
                    {
                        var game = gameDetails[j];
                        result.Games.Add(new GameValue
                        {
                            GameId = j + 1,
                            HomeValue = game != null && game.SettlementStatusID == EventStatusID.CompletedSettlementStatusID ? game.HomeValue : StringResources.HYPHEN,
                            AwayValue = game != null && game.SettlementStatusID == EventStatusID.CompletedSettlementStatusID ? game.AwayValue : StringResources.HYPHEN
                        });
                    }

                    if (numofGame > 13)
                    {
                        var modeValue = numofGame % ConstantValue.NUMBERGAMEOFTENNISSET;
                        if (modeValue > 0)
                        {
                            var moreGame = ConstantValue.NUMBERGAMEOFTENNISSET - modeValue + numofGame;
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
                    : string.Empty;
            }
        }

        public string TotalSetAWay
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue
                    ? this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture)
                    : string.Empty;
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
                if (this.HasResult)
                {
                    string[] betStatusList = this.GetCoreInfoFromBetStatus.Split(',');
                    for (int j = 0; j < betStatusList.Length && j < 5; j++)
                    {
                        var values = betStatusList[j].Trim().Split('-');
                        this.HomeSet[j] = values[0].Trim();
                        this.AwaySet[j] = values[1].Trim();
                        this._totalGameAway += Convert.ToInt32(this.AwaySet[j]);
                        this._totalGameHome += Convert.ToInt32(this.HomeSet[j]);
                    }
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
                return base.VisibleDetailedLink && this.SPORTBOOK.GameDetail != null
                    && this.SPORTBOOK.GameDetail.Any(i => !string.IsNullOrEmpty(i.ResourceId) && i.ResourceId.Trim().Length > 3);
            }
        }

        public bool ShowSet(int set, int betID)
        {
            switch (set)
            {
                case 1:
                    return betID >= BetID.MinSet1GameY && betID <= BetID.MaxSet1GameY;
                case 2:
                    return betID >= BetID.MinSet2GameY && betID <= BetID.MaxSet2GameY;
                case 3:
                    return betID >= BetID.MinSet3GameY && betID <= BetID.MaxSet3GameY;
                case 4:
                    return betID >= BetID.MinSet4GameY && betID <= BetID.MaxSet4GameY;

                case 5:
                    return betID >= BetID.MinSet5GameY;

                default:
                    return false;
            }
        }
    }
}