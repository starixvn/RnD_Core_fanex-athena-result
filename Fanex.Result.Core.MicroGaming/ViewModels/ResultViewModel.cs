namespace Fanex.Result.Core.MicroGaming.ViewModels
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.Serialization;

    [XmlRoot("URL"), XmlType("URL"), Serializable]
    public class ResultViewModel
    {
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Reviewed!")]
        [XmlElement("URL")]
#pragma warning disable S3996 // URI properties should not be strings
        public string Url { get; set; }

#pragma warning restore S3996 // URI properties should not be strings

        [XmlElement("ReturnCode")]
        public int ReturnCode { get; set; }

        [XmlElement("ReturnMessage")]
        public string ReturnMessage { get; set; }
    }
}