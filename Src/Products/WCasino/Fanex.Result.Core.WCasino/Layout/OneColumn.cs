namespace Fanex.Result.Core.WCasino.Layout
{
    using System.Text;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.Extension;

    public class OneColumn : IBaseModel
    {
        public TransDesc TransDesc { get; }

        public string GameId3rd { get; }

        public BetType Bettype { get; }

        public string ImagePath { get; }

        public OneColumn(string transDesc, string gameId3rd, BetType bettype, string imagePath)
        {
            TransDesc = new TransDesc(transDesc);
            GameId3rd = gameId3rd;
            ImagePath = imagePath;
            Bettype = bettype;
        }

        public virtual string GetLayout()
        {
            var strImgUrl = LoadImage(TransDesc.Result);

            return string.Format(GetHtmlLayout(), $"{LangResource.GameID}: {GameId3rd}", LangResource.Results, strImgUrl);
        }

        protected virtual string GetHtmlLayout()
        {
            var strFinalLayout = "<div class='popWTableArea'><table width='100%' cellpadding='0' cellspacing='0' border='0' class='oddsTable info'>";
            strFinalLayout += "<tbody><tr><td class='tabtitle'>{0}</td></tr></tbody>";
            strFinalLayout += "<tbody>";
            strFinalLayout += "<tr align='center'><th>{1}</th></tr>";
            strFinalLayout += "<tr align='center' class='bgcpe'><td>{2}</td></tr>";
            strFinalLayout += "</tbody></table></div>";

            return strFinalLayout;
        }

        protected virtual string LoadImage(string images)
        {
            var strCardImg = new StringBuilder();
            var strImages = images.Split(';');
            foreach (var image in strImages)
            {
                strCardImg.Append($"<img src='{ImagePath}{Extension.GetStringValue(Bettype)}/{image}.png' align='absmiddle' border='0' class='code' width='38' height='38' />");
            }

            return strCardImg.ToString();
        }
    }
}
