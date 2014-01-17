// --------------------------------------------------------------------------
// <copyright file="Default.aspx.cs" company="Nexcel Solutions Vietnam">
// Copyright (coffee) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="" date="">Created</change>
// </history>
namespace Fanex.Result.WebFormTest
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Web.Security;

    /// <summary>
    /// The default page class.
    /// </summary>
    public partial class _Default : System.Web.UI.Page
    {
        /// <summary>
        /// Initialize culture.
        /// </summary>
        protected override void InitializeCulture()
        {
            base.InitializeCulture();
            string selectedLanguage = "zh-TW";
            UICulture = selectedLanguage;
            Culture = selectedLanguage;

            Thread.CurrentThread.CurrentCulture =
                CultureInfo.CreateSpecificCulture(selectedLanguage);
            Thread.CurrentThread.CurrentUICulture = new
                CultureInfo(selectedLanguage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie("alex.nguyen", true);
        }
    }
}