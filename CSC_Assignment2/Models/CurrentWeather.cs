using System;
using System.Xml.Serialization;

namespace CSC_Assignment2.Models
{
    [Serializable]
    public class CurrentWeather
    {
        [XmlElement("Location")]
        public string Location { get; set; }
        [XmlElement("Time")]
        public string Time { get; set; }
        [XmlElement("Wind")]
        public string Wind { get; set; }
        [XmlElement("Visibility")]
        public string Visibility { get; set; }
        [XmlElement("SkyConditions")]
        public string SkyConditions { get; set; }
        [XmlElement("Temperature")]
        public string Temperature { get; set; }
        [XmlElement("DewPoint")]
        public string DewPoint { get; set; }
        [XmlElement("RelativeHumidity")]
        public string RelativeHumidity { get; set; }
        [XmlElement("Pressure")]
        public string Pressure { get; set; }
        [XmlElement("Status")]
        public string Status { get; set; }

    }
}