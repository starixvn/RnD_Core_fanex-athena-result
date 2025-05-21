namespace Fanex.Result.Core.WCasino
{
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.WCasino.Layout;

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
                if (bettype >= (int)BetType.SuperMB111 && bettype <= (int)BetType.SuperMB309){
                    return new Bacarrat(TransDesc, GameId3rd, _ImgPath, refDataHelper, slang).GetLayout();
                }

                if (bettype >= (int)BetType.SuperDT221 && bettype <= (int)BetType.SuperDT301){
                    return new Dragon(TransDesc, GameId3rd, _ImgPath, refDataHelper, slang).GetLayout();
                }

                if (bettype == (int)BetType.WRoulette301){
                    return new OneColumn(TransDesc, GameId3rd, BetType.WRoulette301, _ImgPath).GetLayout();
                }

                if (bettype == (int)BetType.WXD301){
                    return new OneColumn(TransDesc, GameId3rd, BetType.WXD301, _ImgPath).GetLayout();
                }

                if (bettype >= (int)BetType.WSicbo301 && bettype <= (int)BetType.WVietTaiXiu){
                    return new OneColumn(TransDesc, GameId3rd, BetType.WSicbo301, _ImgPath).GetLayout();
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