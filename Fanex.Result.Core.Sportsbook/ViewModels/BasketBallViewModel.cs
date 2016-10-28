namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using Base;
    using Base.DataDomain;
    using Base.ViewModels;
    using DTO;

    public sealed class BasketBallViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public BasketBallViewModel(Sportsbook sportbook, ResultSetting setting)
            : base(sportbook, setting)
        {
        }

        public string QTHome
        {
            get
            {
                var temp = this.GetHomeAwaySet(4, true);
                return Convert.ToInt32(temp) > 0 ? temp : ConstantValue.Hyphen;
            }
        }

        public string QTAway
        {
            get
            {
                var temp = this.GetHomeAwaySet(4, false);
                return Convert.ToInt32(temp) > 0 ? temp : ConstantValue.Hyphen;
            }
        }

        public string HTHomeScore
        {
            get
            {
                return GetScore(SPORTBOOK.HtHomeScore);
            }
        }

        public string HTAwayScore
        {
            get
            {
                return GetScore(SPORTBOOK.HtAwayScore);
            }
        }

        public string FinalHomeScore
        {
            get
            {
                return GetScore(SPORTBOOK.FinalHomeScore);
            }
        }

        public string FinalAwayScore
        {
            get
            {
                return GetScore(SPORTBOOK.FinalAwayScore);
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
                return ConstantValue.Hyphen;
            }

            if (this.HomeSet == null || this.AwaySet == null)
            {
                this.HomeSet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Zero };
                this.AwaySet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Zero };
                if (this.SPORTBOOK.EventStatus != ConstantValue.RefundFT)
                {
                    if (this.HasResult)
                    {
                        string[] betStatusList = this.GetCoreInfoFromBetStatus.Split(',');
                        for (int j = 0; j < betStatusList.Length && j < 5; j++)
                        {
                            var values = betStatusList[j].Trim().Split('-');
                            this.HomeSet[j] = values[0].Trim();
                            this.AwaySet[j] = values[1].Trim();
                        }
                    }
                }
            }

            return isHome ? this.HomeSet[index] : this.AwaySet[index];
        }
    }
}