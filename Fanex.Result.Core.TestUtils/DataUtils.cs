namespace Fanex.Result.Core.TestUtils
{
    using Base;

    public static class DataUtils
    {
        public const string DateTimeFormat = "{0:MM/dd/yyyy<br/>hh:mm:ss tt}";

        public static ResultSetting CreateResultSetting()
            => new ResultSetting
            {
                LanguageCode = "en-US"
            };
    }
}