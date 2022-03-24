namespace Fanex.Result.Core.WM.Layout
{
    using System.Text;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;

    public class AndarBahar : IBaseModel
    {
        protected readonly string ResourceKey = "WM_BT";

        public TransDesc TransDesc { get; }

        public string GameId3rd { get; }

        public string ImagePath { get; }

        public IRefDataHelper RefDataHelper { get; }

        public string Language { get; }

        public string JokerText => RefDataHelper.GetResourceDataByRefIdAndResourceId(ResourceKey, "20022", Language, "Joker");

        public string AndarText => RefDataHelper.GetResourceDataByRefIdAndResourceId(ResourceKey, "20020", Language, "Andar");

        public string BaharText => RefDataHelper.GetResourceDataByRefIdAndResourceId(ResourceKey, "20021", Language, "Bahar");

        public AndarBahar(string transDesc, string gameId3rd, string imagePath, IRefDataHelper refDataHelper, string slang)
        {
            TransDesc = new TransDesc(transDesc);
            GameId3rd = gameId3rd;
            ImagePath = imagePath;
            RefDataHelper = refDataHelper;
            Language = slang;
        }

        public virtual string GetLayout()
        {
            const int firstIndexData = 2;
            var results = ReplaceHardcode(TransDesc.Result).Split(':');
            var jokerData = results.Length > 0 ? results[0] : string.Empty;
            jokerData = jokerData.Length > firstIndexData ? jokerData.Substring(firstIndexData) : string.Empty;
            var andarData = results.Length > 1 ? results[1] : string.Empty;
            andarData = andarData.Length > firstIndexData ? andarData.Substring(firstIndexData) : string.Empty;
            var baharData = results.Length > 2 ? results[2] : string.Empty;
            baharData = baharData.Length > firstIndexData ? baharData.Substring(firstIndexData) : string.Empty;
            var imageJoker = LoadImage(jokerData);
            var imageAndar = LoadImage(andarData);
            var imageBahar = LoadImage(baharData);

            return string.Format(GetHtmlLayout(),
                $"{LangResource.GameID}: {GameId3rd}",
                JokerText,
                imageJoker,
                AndarText,
                imageAndar,
                BaharText,
                imageBahar);
        }

        protected virtual string GetHtmlLayout()
        {
            var strFinalLayout = "<div class='popWTableArea'><table width='100%' cellpadding='0' cellspacing='0' border='0' class='oddsTable info'>";
            strFinalLayout += "<tbody><tr><td colspan='2' class='tabtitle'>{0}</td></tr></tbody>";
            strFinalLayout += "<tbody>";
            strFinalLayout += "<tr align='center'><td colspan='2'>{1}{2}</td></tr>";
            strFinalLayout += "<tr align='center' class='bgcpe'><td width='50%'>{3}<br/>{4}</td><td width='50%'>{5}<br/>{6}</td></tr>";
            strFinalLayout += "</tbody></table></div>";

            return strFinalLayout;
        }

        protected virtual string LoadImage(string images)
        {
            var strCardImg = new StringBuilder();
            var strCard = images.Split(';');
            const int maxCardInLine = 5;
            var i = 1;
            foreach (var card in strCard)
            {
                if (!string.IsNullOrEmpty(card))
                {
                    strCardImg.Append($"<img src='{ImagePath}Poker/{card}.png' align='absmiddle' border='0' class='code' width='38' height='38'/>");
                    i++;
                    if (i > maxCardInLine)
                    {
                        strCardImg.Append("<br/>");
                        i = 1;
                    }
                }
            }

            return strCardImg.ToString();
        }

        protected virtual string ReplaceHardcode(string result)
            => result.Replace("0", string.Empty);
    }
}
