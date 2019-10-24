namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using Base.ViewModels;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using Utilities;

    public sealed class BaseBallViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        public BaseBallViewModel(Sportsbook sportbook, ResultSetting setting)
            : this(sportbook, setting, null)
        {
        }

        public BaseBallViewModel(Sportsbook sportbook, ResultSetting setting, IRefDataHelper refDataHelper)
           : base(sportbook, setting, refDataHelper)
        {
        }

        public string EIAway => GetOverTimeScore(false);

        public string EIHome => GetOverTimeScore(true);

        public string HTAwayScore => GetScore(SPORTBOOK.HtAwayScore);

        public string HTHomeScore => GetScore(SPORTBOOK.HtHomeScore);

        public string FinalAwayScore => GetScore(SPORTBOOK.FinalAwayScore);

        public string FinalHomeScore => GetScore(SPORTBOOK.FinalHomeScore);

        /// <summary>
        /// Get home and away set.
        /// Only used for some sport type such as tennis, badminton....
        /// </summary>
        /// <param name="index">The index of home or away array.</param>
        /// <param name="isHome">True if you want to get value of home set.</param>
        /// <returns>The string value.</returns>
        public override string GetHomeAwaySet(int index, bool isHome)
        {
            const int maxIndex = 9;
            if (index > maxIndex)
            {
                return ConstantValue.Hyphen;
            }

            if (HomeSet == null || AwaySet == null)
            {
                HomeSet = new string[maxIndex + 1];
                AwaySet = new string[maxIndex + 1];

                for (int j = 0; j < maxIndex; j++)
                {
                    HomeSet[j] = ConstantValue.Hyphen;
                    AwaySet[j] = ConstantValue.Hyphen;
                }

                HomeSet[maxIndex] = AwaySet[maxIndex] = "-1";

                GenerateHomeAwaySetValueFromBetStatus(maxIndex + 1);
            }

            return isHome ? HomeSet[index] : AwaySet[index];
        }

        private string GetOverTimeScore(bool isHome)
        {
            var scoreText = GetHomeAwaySet(9, isHome);

            if (!string.IsNullOrEmpty(scoreText) && scoreText.All(char.IsDigit))
            {
                var score = Convert.ToInt32(scoreText);

                return score >= 0 ? score.ToString(CultureInfo.InvariantCulture) : ConstantValue.Hyphen;
            }

            return ConstantValue.Hyphen;
        }
    }
}