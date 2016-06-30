namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Globalization;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.ViewModels;

    public sealed class BasketBallViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public BasketBallViewModel(DTO.Sportsbook sportbook, ResultSetting setting)
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
                return this.SPORTBOOK.HtHomeScore.HasValue ? this.SPORTBOOK.HtHomeScore.Value.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
            }
        }

        public string HTAwayScore
        {
            get
            {
                return this.SPORTBOOK.HtAwayScore.HasValue ? this.SPORTBOOK.HtAwayScore.Value.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
            }
        }

        public string FinalHomeScore
        {
            get
            {
                return this.SPORTBOOK.FinalHomeScore.HasValue ? this.SPORTBOOK.FinalHomeScore.Value.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
            }
        }

        public string FinalAwayScore
        {
            get
            {
                return this.SPORTBOOK.FinalAwayScore.HasValue ? this.SPORTBOOK.FinalAwayScore.Value.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
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