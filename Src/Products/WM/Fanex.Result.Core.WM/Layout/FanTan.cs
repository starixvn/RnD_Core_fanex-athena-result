namespace Fanex.Result.Core.WM.Layout
{
    using System.Text;

    public class FanTan : OneColumn
    {
        public FanTan(string transDesc, string gameId3rd, string imagePath)
            : base(transDesc, gameId3rd, BetType.FanTan, imagePath)
        {
        }

        protected override string LoadImage(string images)
        {
            var strCardImg = new StringBuilder();
            var numberBall = int.Parse(images);
            for (var i = 0; i < numberBall; i++)
            {
                strCardImg.Append($"<img src='{ImagePath}FanTan/ball.png' align='absmiddle' border='0' class='code' width='38' height='38'/>");
            }

            return strCardImg.ToString();
        }
    }
}
