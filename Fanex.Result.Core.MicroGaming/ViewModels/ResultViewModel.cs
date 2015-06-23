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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [XmlRoot("URL"), Serializable]
    public class ResultViewModel
    {
        [XmlElement("ReturnCode")]
        public int ReturnCode { get; set; }

        [XmlElement("ReturnMessage")]
        public string ReturnMessage { get; set; }
    }
}
