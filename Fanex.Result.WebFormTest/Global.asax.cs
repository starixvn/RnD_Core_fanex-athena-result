// --------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Nexcel Solutions Vietnam">
// Copyright (coffee) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="" date="">Created</change>
// </history>
namespace Fanex.Result.WebFormTest
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Global class.
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        private void Application_Start(object sender, EventArgs e)
        {
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        private void Application_End(object sender, EventArgs e)
        {
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        private void Application_Error(object sender, EventArgs e)
        {
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        private void Session_Start(object sender, EventArgs e)
        {
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        private void Session_End(object sender, EventArgs e)
        {
        }
    }
}