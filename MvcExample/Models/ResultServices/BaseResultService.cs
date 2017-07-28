namespace MvcExample.Models.ResultServices
{
    using Fanex.Common.Extensions;
    using Fanex.Data;

    public class BaseResultService
    {
        public BalanceUp GetBalanceUpDay()
        {
            const string UspName = "Lrf_SelBalanceUpDay";

            using (var db = new ObjectDb(UspName))
            {
                var balanceUp = new BalanceUp();

                using (var reader = db.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        balanceUp.BalanceUpDay = reader.ToDateTime("balanceupday");
                        balanceUp.MoveBetsDay = reader.ToDateTime("movebetsday");
                    }

                    return balanceUp;
                }
            }
        }
    }
}