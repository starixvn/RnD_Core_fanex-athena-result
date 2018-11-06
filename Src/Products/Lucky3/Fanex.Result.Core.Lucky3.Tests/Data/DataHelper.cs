namespace Fanex.Result.Core.Lucky3.Tests.Data
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Base.Utilities;
    using NSubstitute;

    public static class DataHelper
    {
        public static DTO.Lucky3 CreateLucky3Data()
            => new DTO.Lucky3
            {
                MatchCode = "1",
                MatchId = 2,
                KickOffTime = DateTime.Parse("01/01/2017"),
                Ball = new List<int> { 1, 2, 3 },
                LiveHomeScore = 1,
                LiveAwayScore = 2,
                FinalHomeScore = 3,
                EventStatus = "completed",
                LeagueId = 11
            };

        public static ResultSetting CreateResultSetting()
            => new ResultSetting
            {
                LanguageCode = "en-US",
                SportType = SportType.Lucky3
            };

        public static IRefDataHelper CreateRefDataHelper()
        {
            var refDataHelper = Substitute.For<IRefDataHelper>();
            refDataHelper.GetLeagueName(11, Arg.Any<string>(), Arg.Any<string>()).Returns("league name");

            return refDataHelper;
        }
    }
}