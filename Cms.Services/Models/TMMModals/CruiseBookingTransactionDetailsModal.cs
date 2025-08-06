using System;

namespace Cms.Services.Models.TMMModals
{
    public class CruiseBookingTransactionDetailsModal
    {
        public long TransactionId { get; set; }
        public string SearchGuid { get; set; }
        public string PNRNo { get; set; }
        public bool? IsBooked { get; set; }
        public DateTime? BookedOn { get; set; }
        public int? PortalID { get; set; }
        public string CruiseTitle { get; set; }
        public string DepartureDate { get; set; }
        public string ArrivalDate { get; set; }
        public DateTime? DepartureDt { get; set; }
        public DateTime? ArrivalDt { get; set; }
        public string Duration { get; set; }
        public string CruiseLineId { get; set; }
        public string CruiseLineName { get; set; }
        public string MasterCruiseID { get; set; }
        public string ShipID { get; set; }
        public string ShipName { get; set; }
        public int? AdultCount { get; set; }
        public double? AdultBasic { get; set; }
        public double? AdultTax { get; set; }
        public double? AdultCommission { get; set; }
        public double? AdultMarkup { get; set; }
        public double? AdultTotalPrice { get; set; }
        public int? ChildCount { get; set; }
        public double? ChildBasic { get; set; }
        public double? ChildTax { get; set; }
        public double? ChildMarkup { get; set; }
        public double? ChildCommission { get; set; }
        public double? ChildTotalPrice { get; set; }
        public double? TotalPrice { get; set; }
        public string CouponCode { get; set; }
        public int? Discount { get; set; }
        public string ToCurrencyType { get; set; }
        public double? FinalTotalPrice { get; set; }
        public double? CurrencyRate { get; set; }
        public string IPAddress { get; set; }
        public string DeviceType { get; set; }
        public string BillerName { get; set; }
        public string BillerAddress { get; set; }
        public string BillerCity { get; set; }
        public string BillerState { get; set; }
        public string BillerZip { get; set; }
        public string BillerEmailId { get; set; }
        public string BillerPhone { get; set; }
        public string BillerCountry { get; set; }
        public string CurrencyType { get; set; }
        public string CardType { get; set; }
        public string PGTransactionId { get; set; }
        public string PGTransStatus { get; set; }
        public string BookingGuid { get; set; }
        public DateTime? InsertedOn { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string CustRefNo { get; set; }
        public string BookingMode { get; set; }
        public int? CreatedBy { get; set; }

    }
}
