namespace Fanex.Result.Core.WM
{
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.WM.Layout;

    public interface IResultPopup
    {
        string genResultPopHtml(string TransDesc, string GameId3rd, string slang, int bettype);
    }

    public class ResultPop : IResultPopup
    {
        private readonly string _ImgPath;
        private readonly IRefDataHelper refDataHelper;

        public ResultPop(string ImgPath, IRefDataHelper refDataHelper)
        {
            _ImgPath = ImgPath;
            this.refDataHelper = refDataHelper ?? ResultCoreManager.RefDataHelper;
        }

        public string genResultPopHtml(string TransDesc, string GameId3rd, string slang, int bettype)
        {
            try
            {
                var bettypeModels = new Dictionary<int, IBaseModel>
                {
                    { (int)BetType.Baccarat,  new Bacarrat(TransDesc, GameId3rd, _ImgPath, refDataHelper, slang)},
                    { (int)BetType.DragonTiger,  new Dragon(TransDesc, GameId3rd, _ImgPath, refDataHelper, slang)},
                    { (int)BetType.Roulette,  new OneColumn(TransDesc, GameId3rd, BetType.Roulette, _ImgPath)},
                    { (int)BetType.Sicbo,  new OneColumn(TransDesc, GameId3rd, BetType.Sicbo, _ImgPath)},
                    { (int)BetType.SeDie,  new OneColumn(TransDesc, GameId3rd, BetType.SeDie, _ImgPath)},
                    { (int)BetType.FanTan,  new FanTan(TransDesc, GameId3rd, _ImgPath)},
                    { (int)BetType.FishPrawnCrab,  new OneColumn(TransDesc, GameId3rd, BetType.FishPrawnCrab, _ImgPath)},
                    { (int)BetType.NiuNiu,  new ThreeColumn(TransDesc, GameId3rd, _ImgPath, refDataHelper, slang)},
                    { (int)BetType.Samgong,  new ThreeColumn(TransDesc, GameId3rd, _ImgPath, refDataHelper, slang)},
                    { (int)BetType.WenzhouPaiGow,  new ThreeColumn(TransDesc, GameId3rd, _ImgPath, refDataHelper, slang)},
                    { (int)BetType.MahjongTiles,  new MahjongTiles(TransDesc, GameId3rd, _ImgPath, refDataHelper, slang)},
                    { (int)BetType.GoldenFlower,  new GoldendFlower(TransDesc, GameId3rd, _ImgPath, refDataHelper, slang)},
                    { (int)BetType.AndarBahar,  new AndarBahar(TransDesc, GameId3rd, _ImgPath, refDataHelper, slang)},
                };

                if (bettypeModels.ContainsKey(bettype))
                {
                    return bettypeModels[bettype].GetLayout();
                }

                return NoInformation();
            }
            catch
            {
                return NoInformation();
            }
        }

        private static string NoInformation()
        {
            var strFinalLayout = "<div class='popWTableArea'><table width='100%' cellpadding='0' cellspacing='0' border='0' class='oddsTable info'>";
            strFinalLayout += "<tbody>";
            strFinalLayout += $"<tr align='center'><td class='noinformation'>{LangResource.NoInfo}</td></tr>";

            return strFinalLayout;
        }
    }

}
