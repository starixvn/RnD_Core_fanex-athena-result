namespace Fanex.Result.Core.WCasino.Layout
{
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;

    public class Bacarrat : TwoColumn
    {
        public Bacarrat(string transDesc, string gameId3rd, string imagePath, IRefDataHelper refDataHelper, string slang)
            : base(transDesc, gameId3rd, imagePath, refDataHelper, slang)
        {
        }

        public override string GetLayout()
        {
            var results = ReplaceHardcode(TransDesc.Result).Split(':');
            var playerImage = LoadImage(results.Length > 0 ? results[1] : string.Empty);
            var bankerImage = LoadImage(results.Length > 1 ? results[0] : string.Empty);

            return string.Format(GetHtmlLayout(), $"{LangResource.GameID}: {GameId3rd}" , PlayerText, BankerText, playerImage, bankerImage);
        }

        private static string ReplaceHardcode(string result)
            => result.Replace("B;", "")
            .Replace("P;", "")
            .Replace("0", "");
    }
}
