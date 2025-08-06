namespace Cms.Services.Filters.TMM
{
    public class BookingJourneyDetailsFilter:CommonFilter
    {
        public long? TransactionId { get; set; }
        public string JourneyType { get; set; }
        public string OriginAirportCode { get; set; }
        public string OriginAirportName { get; set; }
        public string OriginCountryCode { get; set; }
        public string OriginCountryName { get; set; }
        public string OriginCityCode { get; set; }
        public string OriginCityName { get; set; }
        public string DestAirportCode { get; set; }
        public string DestAirportName { get; set; }
        public string DestCountryCode { get; set; }
        public string DestCountryName { get; set; }
        public string DestCityCode { get; set; }
        public string DestCityName { get; set; }
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public string AirlineClass { get; set; }
        public string FlightName { get; set; }
        public string CabinClass { get; set; }
        public string FlightStatus { get; set; }

    }
}
