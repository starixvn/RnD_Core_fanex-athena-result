using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fanex.Result.Core;
using System.Configuration;

namespace Fanex.Result.WebFormTest
{
    public partial class KenoResult : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            var setting = new KenoResultSetting
            {
                Language = "en",
                SiteID = ConfigurationManager.AppSettings["KenoSiteID"],
                AuthorizedUrl = ConfigurationManager.AppSettings["KenoAuthorizedUrl"],
                ExpireTokenUrl = ConfigurationManager.AppSettings["KenoExpireTokenUrl"],
                ExtendTokenUrl = ConfigurationManager.AppSettings["KenoExtendTokenUrl"],
                VenderKey = ConfigurationManager.AppSettings["KenoVenderKey"],
                Vendor = ConfigurationManager.AppSettings["KenoVendor"],
                ResultUrl = ConfigurationManager.AppSettings["KenoResultUrl"],
                KickOutUrl = string.Empty
            };

            var currentUserId = Request.Form["userId"];
            if (!string.IsNullOrEmpty(currentUserId))
            {
                string err = string.Empty;
                ResultEngine.KeepKenoLived(setting, currentUserId, out err);
            }
            else
            {

                this.KenoReulst.Text = ResultEngine.KenoHtmlParser(setting, "63", DateTime.Now);
            }
        }
    }
}