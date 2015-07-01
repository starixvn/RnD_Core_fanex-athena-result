// -----------------------------------------------------------------------
// <copyright file="ResultViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.MicroGaming.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [XmlRoot("URL"), XmlType("URL"), Serializable]
    public class ResultViewModel
    {
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Reviewed!")]
        [XmlElement("URL")]
        public string Url { get; set; }

        [XmlElement("ReturnCode")]
        public int ReturnCode { get; set; }

        [XmlElement("ReturnMessage")]
        public string ReturnMessage { get; set; }
    }
}
