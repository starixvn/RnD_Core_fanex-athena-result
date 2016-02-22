namespace Fanex.Result.Core.AllBetCasino
{
    using System;
    using System.Configuration;
    using System.Web;
    using LiveCasinoResult;

    public class BuilderService : IBuilderService
    {
        private const string InvalidTransIdErrorMessage = "The TransId is invalid!";

        private const string InvalidConfigErrorMessage = "Not found AllBetCasino Configuration";

        public virtual string GetConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Fanex.Result.Core.3rdLiveCasino"].ConnectionString;
            }
        }

        public virtual string GetTemplatePath
        {
            get
            {
                return ConfigurationManager.AppSettings["Fanex.Result.Core.3rdLiveCasinoTplPath"];
            }
        }

        public string Render(string transID, string languageCode = "en")
        {
            if (string.IsNullOrWhiteSpace(transID) || transID.Contains(" "))
            {
                throw new AllBetCasinoException(InvalidTransIdErrorMessage);
            }

            var conn = GetConnectionString;
            var tplPath = GetTemplatePath;

            if (string.IsNullOrEmpty(conn) || string.IsNullOrEmpty(tplPath))
            {
                throw new AllBetCasinoException(InvalidConfigErrorMessage);
            }

            try
            {
                ResultPop resultContext = new ResultPop(HttpContext.Current, conn, tplPath);
                return resultContext.genResultPopHtmlbyID(transID, languageCode);
            }
            catch (Exception exception)
            {
                throw new AllBetCasinoException(exception.Message, exception.InnerException);
            }
        }
    }
}
