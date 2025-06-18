namespace Fanex.Result.Core.WCasino.Layout
{
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;

    public class Dragon : TwoColumn
    {
        public Dragon(string transDesc, string gameId3rd, string imagePath, IRefDataHelper refDataHelper, string slang)
            : base(transDesc, gameId3rd, imagePath, refDataHelper, slang)
        {
        }

        public override string GetLayout()
        {
            var results = ReplaceHardcode(TransDesc.Result).Split(':');
            var dragonImage = LoadImage(results.Length > 0 ? results[0] : string.Empty);
            var tigerImage = LoadImage(results.Length > 1 ? results[1] : string.Empty);

            return string.Format(GetHtmlLayout(), $"{LangResource.GameID}: {GameId3rd}" , DragonText, TigerText, dragonImage, tigerImage);
        }

        private static string ReplaceHardcode(string result)
            => result.Replace("D;", "")
            .Replace("T;", "")
            .Replace("0", "");
    }
}
