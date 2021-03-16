namespace Fanex.Result.Core.SportsLottery
{
    using System.Collections.Generic;

    public static class SportsLotteryConstants
    {
        public static class Device
        {
            public const string Mobile = "M";
            public const string Desktop = "D";
        }

        public const string DefaultLanguage = "en-US";

        private static readonly Dictionary<string, string> SupportedLanguages = new Dictionary<string, string>
        {
            { "en-US", "en" },
            { "zh-CN", "cs" },
            { "zh-TW", "ch" },
            { "zh-Hans", "zhcn" },
            { "th-TH", "th" },
            { "vi-VN", "vn" },
            { "id-ID", "id" }
        };


        public static string GetLanguageCode(string language)
            => SupportedLanguages.ContainsKey(language)
            ? SupportedLanguages[language]
            : SupportedLanguages[DefaultLanguage];
    }
}
