namespace Fanex.Result.Core.WM.Layout
{
    using System.Text;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.WM.Resource;

    public class ThreeColumn : IBaseModel
    {
        protected readonly string ResourceKey = "WM_BT";

        public TransDesc TransDesc { get; }

        public string GameId3rd { get; }

        public string ImagePath { get; }

        public IRefDataHelper RefDataHelper { get; }

        public string Language { get; }

        public string BankerText => RefDataHelper.GetResourceDataByRefIdAndResourceId(ResourceKey, "10001", Language, "Banker");

        public ThreeColumn(string transDesc, string gameId3rd, string imagePath, IRefDataHelper refDataHelper, string slang)
        {
            TransDesc = new TransDesc(transDesc);
            GameId3rd = gameId3rd;
            ImagePath = imagePath;
            RefDataHelper = refDataHelper;
            Language = slang;
        }

        public virtual string GetLayout()
        {
            var results = ReplaceHardcode(TransDesc.Result).Split(':');
            var imageBanker = LoadImage(results.Length > 0 ? results[0] : string.Empty);
            var imagePlayer1 = LoadImage(results.Length > 1 ? results[1] : string.Empty);
            var imagePlayer2 = LoadImage(results.Length > 2 ? results[2] : string.Empty);
            var imagePlayer3 = LoadImage(results.Length > 3 ? results[3] : string.Empty);

            return string.Format(GetHtmlLayout(), 
                $"{LangResource.GameID}: {GameId3rd}", 
                BankerText,
                imageBanker,
                WMResource.Player1,
                imagePlayer1,
                WMResource.Player2,
                imagePlayer2,
                WMResource.Player3,
                imagePlayer3);
        }

        protected virtual string GetHtmlLayout()
        {
            var strFinalLayout = "<div class='popWTableArea'><table width='100%' cellpadding='0' cellspacing='0' border='0' class='oddsTable info'>";
            strFinalLayout += "<tbody><tr><td class='tabtitle' colspan='3'>{0}</td></tr></tbody>";
            strFinalLayout += "<tbody><tr align='center'><td colspan='3'>{1}: {2}</td></tr>";
            strFinalLayout += "<tr align='center' class='bgcpe'><td>{3}<br/>{4}</td>";
            strFinalLayout += "<td>{5}<br/>{6}</td>";
            strFinalLayout += "<td>{7}<br/>{8}</td></tr>";

            return strFinalLayout;
        }

        protected virtual string LoadImage(string images)
        {
            var strCardImg = new StringBuilder();
            var strCard = images.Split(';');
            foreach (var card in strCard)
            {
                strCardImg.Append($"<img src='{ImagePath}Poker/{card}.png' align='absmiddle' border='0' class='code' width='38' height='38'/>");
            }

            return strCardImg.ToString();
        }

        protected virtual string ReplaceHardcode(string result)
            => result.Replace("B;", string.Empty)
            .Replace("P;", string.Empty)
            .Replace("P1;", string.Empty)
            .Replace("P2;", string.Empty)
            .Replace("P3;", string.Empty)
            .Replace("0", string.Empty);
    }
}
