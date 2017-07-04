namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using Base.ViewModels;
    using DTO;

    public sealed class HockeyViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public HockeyViewModel(Sportsbook sportbook, ResultSetting setting)
            : base(sportbook, setting)
        {
        }

        public string OTHome => GetOverTimeScore(true);

        public string OTAway => GetOverTimeScore(false);

        public string FinalHomeScore => GetScore(SPORTBOOK.FinalHomeScore);

        public string FinalAwayScore => GetScore(SPORTBOOK.FinalAwayScore);

        /// <summary>
        /// Get home and away set.
        /// Only used for some sport type such as tennis, badminton....
        /// </summary>
        /// <param name="index">The index of home or away array.</param>
        /// <param name="isHome">True if you want to get value of home set.</param>
        /// <returns>The string value.</returns>
        public override string GetHomeAwaySet(int index, bool isHome)
        {
            if (index > 3)
            {
                return ConstantValue.Hyphen;
            }

            if (AwaySet == null || HomeSet == null)
            {
                HomeSet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Zero };
                AwaySet = new[] { ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Hyphen, ConstantValue.Zero };

                GenerateHomeAwaySetValueFromBetStatus(4);
            }

            return isHome ? HomeSet[index] : AwaySet[index];
        }

        private string GetOverTimeScore(bool isHome)
        {
            var scoreText = GetHomeAwaySet(3, isHome);

            if (!string.IsNullOrEmpty(scoreText) && scoreText.All(char.IsDigit))
            {
                var score = Convert.ToInt32(scoreText);

                return score > 0 ? score.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
            }

            return ConstantValue.Hyphen;
        }
    }
}