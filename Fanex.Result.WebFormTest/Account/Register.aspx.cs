// --------------------------------------------------------------------------
// <copyright file="Register.aspx.cs" company="Nexcel Solutions Vietnam">
// Copyright (coffee) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="" date="">Created</change>
// </history>
namespace Fanex.Result.WebFormTest.Account
{
    using System;
    using System.Web.Security;

    /// <summary>
    /// The register class.
    /// </summary>
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="sender">The sender parameters.</param>
        /// <param name="e">The events..</param>
        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (string.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }

            Response.Redirect(continueUrl);
        }
    }
}