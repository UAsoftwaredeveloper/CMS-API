using System.Collections.Generic;
using System.Xml.Serialization;

namespace Cms.Services.Filters
{
    [XmlRoot("AirSearchQuery")]
    public class FlightSearchDetails
    {
        [XmlElement("Master")]
        public Master Master { get; set; }

        [XmlElement("JourneyType")]
        public string JourneyType { get; set; }

        [XmlElement("DeviceName")]
        public string DeviceName { get; set; }

        [XmlElement("Currency")]
        public string Currency { get; set; }

        [XmlArray("Segments")]
        [XmlArrayItem("Segment")] public List<Segment> Segments { get; set; }

        [XmlElement("PaxDetail")]
        public PaxDetail PaxDetail { get; set; }

        [XmlElement("Flexi")]
        public int Flexi { get; set; }

        [XmlElement("IsAltDate")]
        public int IsAltDate { get; set; }

        [XmlElement("IsNearByAirport")]
        public int IsNearByAirport { get; set; }

        [XmlElement("Direct")]
        public int Direct { get; set; }

        [XmlElement("Cabin")]
        public Cabin Cabin { get; set; }

        [XmlElement("Airlines")]
        public Airlines Airlines { get; set; }

        [XmlElement("Authentication")]
        public Authentication Authentication { get; set; }
    }

    public class Master
    {
        [XmlElement("CompanyId")]
        public string CompanyId { get; set; }

        [XmlElement("AgentId")]
        public string AgentId { get; set; }

        [XmlElement("BranchId")]
        public string BranchId { get; set; }

        [XmlElement("CoustmerType")]
        public string CoustmerType { get; set; }

        [XmlElement("LangCode")]
        public string LangCode { get; set; }
    }

    public class Segment
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("Origin")]
        public string Origin { get; set; }

        [XmlElement("Destination")]
        public string Destination { get; set; }

        [XmlElement("Date")]
        public string Date { get; set; }
    }

    public class PaxDetail
    {
        [XmlElement("NoAdult")]
        public int NoAdult { get; set; }

        [XmlElement("NoChild")]
        public int NoChild { get; set; }

        [XmlElement("NoInfant")]
        public int NoInfant { get; set; }

        [XmlElement("NoInfantOfSeat")]
        public int NoInfantOfSeat { get; set; }
    }

    public class Cabin
    {
        [XmlElement("Class")]
        public string Class { get; set; }
    }

    public class Airlines
    {
        [XmlElement("Airline")]
        public string Airline { get; set; }
    }

    public class Authentication
    {
        [XmlElement("HAP")]
        public string HAP { get; set; }

        [XmlElement("HapPassword")]
        public string HapPassword { get; set; }

        [XmlElement("HapType")]
        public string HapType { get; set; }
    }
}
