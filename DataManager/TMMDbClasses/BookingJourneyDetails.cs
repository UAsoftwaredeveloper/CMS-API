using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.TMMDbClasses
{
    [Table("BookingJourneyDetails")]
    public class BookingJourneyDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey(nameof(BookingTransactionDetails))]
        public long? TransactionId { get; set; }
        public string JourneyType { get; set; }
        public int? JourneyOrder { get; set; }
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
        public string ValidatingAirlineCode { get; set; }
        public string ValidatingAirlineName { get; set; }
        public string OperatingAirlineCode { get; set; }
        public string OperatingAirlineName { get; set; }
        public DateTime? DepartureDate { get; set; }
        public string DepartureTerminal { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string ArrivalTerminal { get; set; }
        public string BaggageAllowance { get; set; }
        public string CabinClass { get; set; }
        public int? FlightTripTime { get; set; }
        public string FlightStatus { get; set; }
        public BookingTransactionDetails BookingTransactionDetails { get; set; }
    }
}
