using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Fanex.Result.Core.AllBetCasino
{
    public class BuilderService : IBuilderService
    {
        private const string InvalidTransIdErrorMessage = "The TransId is invalid!";


        public string Render(string transID, string languageCode = "en")
        {
            if (string.IsNullOrWhiteSpace(transID) || transID.Contains(" "))
            {
                throw new AllBetCasinoException(InvalidTransIdErrorMessage);
            }

            var connectionString = ConfigurationManager.ConnectionStrings["Fanex.Result.Core.3rdLiveCasino"].ConnectionString;
            var templatePath = ConfigurationManager.AppSettings["Fanex.Result.Core.3rdLiveCasinoTplPath"];


            return string.Empty;
        }
    }
}
