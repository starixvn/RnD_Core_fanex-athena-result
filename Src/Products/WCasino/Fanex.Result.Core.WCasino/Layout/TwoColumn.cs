namespace Fanex.Result.Core.WCasino.Layout
{
    using System;
    using System.Text;
    using Fanex.Result.Core.Base.Utilities;

    public class TwoColumn : IBaseModel
    {
        protected readonly string ResourceKey = "WLC_BC";

        public TransDesc TransDesc { get; }

        public string GameId3rd { get; }

        public string ImagePath { get; }

        public IRefDataHelper RefDataHelper { get; }

        public string Language { get; }

        public string BankerText => RefDataHelper.GetResourceDataByRefIdAndResourceId(ResourceKey, "BAC_BANKER", Language, "Banker");

        public string PlayerText => RefDataHelper.GetResourceDataByRefIdAndResourceId(ResourceKey, "BAC_PLAYER", Language, "Player");

        public string DragonText => RefDataHelper.GetResourceDataByRefIdAndResourceId(ResourceKey, "DT_DRAGON", Language, "Dragon");

        public string TigerText => RefDataHelper.GetResourceDataByRefIdAndResourceId(ResourceKey, "DT_TIGER", Language, "Tiger");

        public TwoColumn(string transDesc, string gameId3rd, string imagePath, IRefDataHelper refDataHelper, string slang)
        {
            TransDesc = new TransDesc(transDesc);
            GameId3rd = gameId3rd;
            ImagePath = imagePath;
            RefDataHelper = refDataHelper;
            Language = slang;
        }

        public virtual string GetLayout()
            => throw new NotImplementedException();

        protected virtual string GetHtmlLayout()
        {
            var strFinalLayout = "<div class='popWTableArea'><table width='100%' cellpadding='0' cellspacing='0' border='0' class='oddsTable info'>";
            strFinalLayout += "<tbody><tr><td colspan='2' class='tabtitle'>{0}</td></tr></tbody>";
            strFinalLayout += "<tbody>";
            strFinalLayout += "<tr align='center'><th>{1}</th><th>{2}</th></tr>";
            strFinalLayout += "<tr align='center' class='bgcpe'><td width='50%'>{3}</td><td width='50%'>{4}</td></tr>";
            strFinalLayout += "</tbody></table></div>";

            return strFinalLayout;
        }

        protected virtual string LoadImage(string images)
        {
            var strCardImg = new StringBuilder();
            var strCard = images.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var card in strCard)
            {
                strCardImg.Append($"<img src='{ImagePath}Poker/{card}.png' align='absmiddle' border='0' class='code' width='38' height='38'/>");
            }

            return strCardImg.ToString();
        }
    }
} 