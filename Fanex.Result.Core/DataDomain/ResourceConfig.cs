// -----------------------------------------------------------------------
// <copyright file="ResourceConfig.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.DataDomain
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal static class ResourceConfig
    {
        public const string EXCELTEMPLATEPATH = "App_Data/Fanex.Result/";
        public const string RESOURCEPATH = "App_Data/Fanex.Result.Resource/";
        public const string SPORTTYPEARGUEMENTMES = "Sport Type is invalid or DTO ({0}) doesn't match with sportType";
        public const string ARGUEMENTMESOUTRIGHT = "DTO ({0}) is invalid";
        public const string ENGLISHLANGCODE = "en-US";
        public const string LANGUAGEARGUEMENTMES = "Fanex.Result.Core doesn't support this language {0}";
        public static readonly string[] LangCodes = { "en-US", "ja-JP", "ko-KR", "vi-VN", "zh-CN", "zh-TW" };
        public const string UNSUPPORTDTOARGUEMENTMES = "We still don't support DTO {0}";
        public const string UNSUPPORTSPORTTYPEARGUEMENTMES = "We still don't support this sport [{0}]";

    }
}