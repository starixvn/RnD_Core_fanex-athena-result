namespace Fanex.Result.Core.CockFighting
{
    public interface ICockFightingAPI
    {
        /// <summary>
        /// Get Result URL token.
        /// </summary>
        /// <param name="customerId">The customer ID of whoever is logged in.</param>
        /// <param name="languageCode">The language code.</param>
        /// <returns>The result URL.</returns>
        string GetResultToken(string customerId, string languageCode = "en");

        /// <summary>
        /// Extend authorized token.
        /// </summary>
        /// <param name="customerId">The customer ID of whoever is logged in.</param>
        /// <returns>The JSON Result.</returns>
        string ExtendToken(string customerId);
    }
}