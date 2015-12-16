// -----------------------------------------------------------------------
// <copyright file="CockFighting.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.CockFighting
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class Config
    {
        /// <summary>
        /// Gets or sets the site ID.
        /// Gets site Id list from here :
        /// <paramref name="Site ID List" value="https://redmine.nexcel.vn/projects/cock-fighting-sabong1/wiki/Result_Core"/>
        /// </summary>
        /// <value>The site ID.</value>
        public string SiteId { get; set; }

        public string Vendor { get; set; }
        public string VendorKey { get; set; }
        public string AuthorizedAPI { get; set; }
        public string ExtendedAPI { get; set; }
    }
}