using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.TMMDbClasses
{
    [Table("BookingTransactionDetails")]
    public class BookingTransactionDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TransactionId { get; set; }
        public int? PortalID { get; set; }
        public string SearchGuid { get; set; }
        public string PNRNo { get; set; }
        public bool? IsBooked { get; set; }
        public DateTime? BookedOn { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime? DepartDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string TripType { get; set; }
        public string CabinClass { get; set; }
        public string SupplierName { get; set; }
        public string FareType { get; set; }
        public int? AdultCount { get; set; }
        public double? AdultBasic { get; set; }
        public double? AdultTax { get; set; }
        public double? AdultCommission { get; set; }
        public double? AdultMarkup { get; set; }
        public double? AdultTotalPrice { get; set; }
        public int? ChildCount { get; set; }
        public double? ChildBasic { get; set; }
        public double? ChildTax { get; set; }
        public double? ChildCommission { get; set; }
        public double? ChildMarkup { get; set; }
        public double? ChildTotalPrice { get; set; }
        public int? InfantCount { get; set; }
        public double? InfantBasic { get; set; }
        public double? InfantTax { get; set; }
        public double? InfantCommission { get; set; }
        public double? InfantMarkup { get; set; }
        public double? InfantTotalPrice { get; set; }
        public int? InfantInLapCount { get; set; }
        public double? InfantInLapBasic { get; set; }
        public double? InfantInLapTax { get; set; }
        public double? InfantInLapCommission { get; set; }
        public double? InfantInLapMarkup { get; set; }
        public double? InfantInLapTotalPrice { get; set; }
        public double? TotalPrice { get; set; }
        public string IPAddress { get; set; }
        public string DeviceType { get; set; }
        public string UTMSource { get; set; }
        public string BillerName { get; set; }
        public string BillerCity { get; set; }
        public string BillerAddress { get; set; }
        public string BillerState { get; set; }
        public string BillerZip { get; set; }
        public string BillerCountry { get; set; }
        public string BillerPhone { get; set; }
        public string BillerEmailId { get; set; }
        public string BillerAddress2 { get; set; }
        public string BillerAlternatePhone { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmailId { get; set; }
        public string CurrencyType { get; set; }
        public string Floor { get; set; }
        public string Apartment { get; set; }
        public string House { get; set; }
        public double? FinalTotalPrice { get; set; }
        public float? CurrencyRate { get; set; }
        public string PGTransactionId { get; set; }
        public string PGTransStatus { get; set; }
        public string CardType { get; set; }
        public DateTime? InsertedOn { get; set; }
        public string CouponCode { get; set; }
        public int? Discount { get; set; }
        public string Origin1 { get; set; }
        public string Destination1 { get; set; }
        public string Origin2 { get; set; }
        public string Destination2 { get; set; }
        public DateTime? DepartDate1 { get; set; }
        public DateTime? DepartDate2 { get; set; }
        public string ToCurrencyType { get; set; }
        public string GDSConfirmationNo { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string PNR_Live { get; set; }
        public string CustRefNo { get; set; }
        public string IsAlternateNearbyContract { get; set; }
        public string AssistancePackageType { get; set; }
        public double? AssistancePackageCharge { get; set; }
        public bool? LostBaggageProtectionIncluded { get; set; }
        public double? LostBaggageProtectionCharges { get; set; }
        public bool? TravelProtectionIncluded { get; set; }
        public double? TravelProtectionCharges { get; set; }
        public bool? RefundBookingIncluded { get; set; }
        public double? RefundBookingCharges { get; set; }
        public bool? PriceMatchIncluded { get; set; }
        public double? PriceMatchCharges { get; set; }
        public bool? TicketFlightWatcherIncluded { get; set; }
        public double? TicketFlightWatcherCharges { get; set; }
        public bool? WebCheckInIncluded { get; set; }
        public double? WebCheckInCharges { get; set; }
        public string AdultBaggageInfo { get; set; }       
        public string AdultCabinBaggageInfo { get; set; }  
        public string ChildBaggageInfo { get; set; }
        public string ChildCabinBaggageInfo { get; set; }
        public string InfantBaggageInfo { get; set; }
        public string InfantCabinBaggageInfo { get; set; }
        public string InfantInLapBaggageInfo { get; set; }
        public string InfantInLapCabinBaggageInfo { get; set; }
        public string BookingConfirmStatus { get; set; }
        public virtual ICollection<BookingJourneyDetails> BookingJourneyDetails { get; set; }
        public virtual ICollection<BookingPaxDetails> BookingPaxDetails { get; set; }
        public long? CreatedBy { get; set; }

    }
}
