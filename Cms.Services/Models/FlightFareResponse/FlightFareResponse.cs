using Cms.Services.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Services.Models.FlightFareResponse
{
    public class Itinerary
    {
        public string Key { get; set; }
        public string BaseFare { get; set; }
        public string Taxes { get; set; }
        public string TotalPrice { get; set; }
        public string markUp { get; set; }
        public decimal GrandTotal { get; set; }
        public string Commission { get; set; }

        public string Safi { get; set; }
        public string ExactTotalPrice { get; set; }

        //public string Currency { get; set; }
        public Currency Currency { get; set; }
        public string FareType { get; set; }
        public int IndexNumber { get; set; }
        public string Provider { get; set; }
        public string ValCarrier { get; set; }
        public string LastTicketingDate { get; set; }
        public Adult Adult { get; set; }
        public Child Child { get; set; }
        public Infant Infant { get; set; }
        public InfantOfSeat InfantOfSeat { get; set; }
        public string Refundable { get; set; }
        public Sectors Sectors { get; set; }
        public FareBasisCodes FareBasisCodes { get; set; }
        public int OutBound { get; set; }
        public int InBound { get; set; }
        public bool IsSoldOut { get; set; }
        public bool IsOpaqueAirline { get; set; }
        public string IsAltDate { get; set; }
        public string IsNearBy { get; set; }
        public string GroupPriority { get; set; }
    }


    public class Currency
    {
        public string ClientCurrency { get; set; }
        public string ROE { get; set; }
        public string text { get; set; }
        public string Supplier { get; set; }
        public string Client { get; set; }
    }

    public class FareBasisCodes
    {
        public List<FareBasiCode> FareBasiCode { get; set; }
    }

    public class FareBasiCode
    {
        public string FareBasis { get; set; }
        public string Airline { get; set; }
        public string PaxType { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string FareInfoKeyKey { get; set; }
        public string FareInfoKeyText { get; set; }
        //public FareInfoKey FareInfoKey { get; set; }
    }

    public class FareInfoKey
    {
        public string Key { get; set; }
        public string text { get; set; }
    }
    public class Sectors
    {
        public List<Sector> Sector { get; set; }
    }
    public class Sector
    {
        public string isConnect { get; set; }
        public string Group { get; set; }
        public string Key { get; set; }
        public string AirV { get; set; }
        public string AirlineName { get; set; }
        public string AirlineLogoPath { get; set; }
        public string Class { get; set; }

        public CabinClassWp CabinClass { get; set; }
        public string NoSeats { get; set; }
        public string FltNum { get; set; }
        public string EquipType { get; set; }
        public string ChangePlane { get; set; }
        public string ElapsedTime { get; set; }
        public string ActualTime { get; set; }
        public string GroundTime { get; set; }

        public Departure Departure { get; set; }
        public Arrival Arrival { get; set; }
        public string Status { get; set; }
        public string isReturn { get; set; }
        public string OptrCarrier { get; set; }
        public string OptrCarrierDes { get; set; }
        public string MrktCarrier { get; set; }
        public string MrktCarrierDes { get; set; }
        public string TechStopOver { get; set; }="0";
        public TechStopOverDetail TechStopOverDetail { get; set; } = new TechStopOverDetail
        {
            DepartureAirport = "",
            DepartureTerminal = "",
            GroundTime = "",
        };
        public string BaggageInfo { get; set; }
        public TransitTime TransitTime { get; set; }
        public string SegmentIndex { get; set; }
        public string BrandName { get; set; }
        public bool IsOpaqueAirline { get; set; }
    }
    public class Adult
    {
        public int NoAdult { get; set; }
        public string markUp { get; set; }
        public string AdtBFare { get; set; }
        public string AdTax { get; set; }
        public string Commission { get; set; }

    }
    public class Child
    {
        public int NoChild { get; set; }
        public string markUp { get; set; }
        public string ChdBFare { get; set; }
        public string CHTax { get; set; }
        public string Commission { get; set; }
    }

    public class Infant
    {
        public int NoInfant { get; set; }
        public string markUp { get; set; }
        public string InfBFare { get; set; }
        public string InTax { get; set; }
        public string Commission { get; set; }
    }
    public class InfantOfSeat
    {
        public int NoInfantOfSeat { get; set; }
        public string markUp { get; set; }
        public string InsBFare { get; set; }
        public string InsTax { get; set; }
        public string Commission { get; set; }
    }
    public class CabinClassWp
    {
        public string Code { get; set; }
        public string Des { get; set; }
    }
    public class Departure
    {
        public string Terminal { get; set; }
        public string AirpCode { get; set; }
        public string AirpName { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
    public class Arrival
    {
        public string Terminal { get; set; }
        public string AirpCode { get; set; }
        public string AirpName { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
    public class OptrCarrier
    {
        public string OptrCarrierDes { get; set; }
        public string text { get; set; }
    }
    public class MrktCarrier
    {
        public string MrktCarrierDes { get; set; }
    }

    public class TransitTime
    {
        public string time { get; set; }
        public string text { get; set; }
        public string TimeDes { get; set; }
    }
    public class FareBasisCodeEL
    {
        private string _farebasis = string.Empty;
        public string FareBasis
        {
            get { return _farebasis; }
            set { _farebasis = value; }
        }
        private string _airline = string.Empty;
        public string Airline
        {
            get { return _airline; }
            set { _airline = value; }
        }
        private string _paxtype = string.Empty;
        public string PaxType
        {
            get { return _paxtype; }
            set { _paxtype = value; }
        }
        private string _origin = string.Empty;
        public string Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }
        private string _destination = string.Empty;
        public string Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }
        private string _farerst = string.Empty;
        public string FareRst
        {
            get { return _farerst; }
            set { _farerst = value; }
        }
    }

    public class FareRulesResponseWrapper
    {
        public FareRulesResponse FareRulesResponse { get; set; }
    }
    public class FareRulesResponse
    {
        public List<FareRule> FareRuleList { get; set; }

    }

    public class FareRule
    {
        public List<Heading> HeadingList { get; set; }
        public string AirlineCode { get; set; }
    }
    public class Heading
    {
        public string Value { get; set; }
        public List<Text> Text { get; set; }
    }
    public class Text
    {
        public string TextValue { get; set; }
    }

    public class BaggageFeesLinksResponse
    {
        public List<Airlines> AirlineBaggageList { get; set; }

    }
    public class TechStopOverDetail
    {
        public string DepartureTerminal { get; set; }
        public string DepartureAirport { get; set; }
        public string GroundTime { get; set; }
    }
}
