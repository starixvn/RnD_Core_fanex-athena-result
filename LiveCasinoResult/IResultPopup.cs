namespace LiveCasinoResult
{
    public interface IResultPopup
    {
        string genResultPopHtmlbyID(string TransID, string slang);

        string genResultPopHtml(string TransDesc, string GameId3rd, string slang, string bettype);
    }
}