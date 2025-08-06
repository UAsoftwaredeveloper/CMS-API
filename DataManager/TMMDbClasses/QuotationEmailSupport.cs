namespace DataManager.TMMDbClasses
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("QuotationEmailSupport")]
    public class QuotationEmailSupport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string OrigineAirport { get; set; }
        public string OrigineAirportCode { get; set; }
        public string OrigineAirportCityCode { get; set; }
        public string OrigineAirportCityName { get; set; }
        public string OrigineAirportCountryCode { get; set; }
        public string OrigineAirportCountryName { get; set; }
        public string DestinationAirport { get; set; }
        public string DestinationAirportCode { get; set; }
        public string DestinationAirportCityCode { get; set; }
        public string DestinationAirportCityName { get; set; }
        public string DestinationAirportCountryCode { get; set; }
        public string DestinationAirportCountryName { get; set; }
        public int AdultCount { get; set; }
        public int? ChildCount { get; set; }
        public int DayCount { get; set; }
        public string EnqueryDetails { get; set; }
        public string CustomerIp { get; set; }
        public string DeviceType { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? PortalId { get; set; }
        public int? UserId { get; set; }
        public string UserEmail { get; set; }
    }
}
