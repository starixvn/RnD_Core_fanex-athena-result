namespace Fanex.Result.Core.WM.Layout
{
    using System.Text;
    using Fanex.Result.Core.Base.Utilities;

    public class MahjongTiles : ThreeColumn
    {
        public MahjongTiles(string transDesc, string gameId3rd, string imagePath, IRefDataHelper refDataHelper, string slang)
            : base(transDesc, gameId3rd, imagePath, refDataHelper, slang)
        {
        }

        protected override string LoadImage(string images)
        {
            var strCardImg = new StringBuilder();
            var strImages = images.Split(';');
            foreach (var image in strImages)
            {
                strCardImg.Append($"<img src='{ImagePath}MahjongTiles/{image}.png' align='absmiddle' border='0' class='code' width='38' height='38' />");
            }

            return strCardImg.ToString();
        }

        protected override string ReplaceHardcode(string result)
            => result.Replace("W", "0")
            .Replace("B;", "")
            .Replace("P1;", "")
            .Replace("P2;", "")
            .Replace("P3;", "");

    }
}
