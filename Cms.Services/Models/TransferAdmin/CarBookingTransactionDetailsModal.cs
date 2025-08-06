using System;

namespace Cms.Services.Models.TransferAdmin
{

    public class CarBookingTransactionDetailsModal
    {
        public int BookingID { get; set; }
        public int? PortalID { get; set; }
        public string SearchKey { get; set; }
        public string RepriceKey { get; set; }
        public int? AffiliateID { get; set; }
        public int? SupplierID { get; set; }
        public string Addres { get; set; }
        public string TripType { get; set; }
        public string From { get; set; }
        public string FromType { get; set; }
        public string To { get; set; }
        public string ToType { get; set; }
        public string PickUpDate { get; set; }
        public string DropDate { get; set; }
        public string PickUpTime { get; set; }
        public string DropTime { get; set; }
        public string ArrivalFlightCode { get; set; }
        public string DepatureFlightCode { get; set; }
        public int? AdultCount { get; set; }
        public int? ChildCount { get; set; }
        public string BookedOn { get; set; }
        public double? TotalPrice { get; set; }
        public double? Markup { get; set; }
        public double? SupplierBaseFare { get; set; }
        public string BookingStatus { get; set; }
        public string Currency { get; set; }
        public string IPAddress { get; set; }
        public string BillerEmailId { get; set; }
        public string BillerPhone { get; set; }
        public string BillerName { get; set; }
        public string CountryCode { get; set; }
        public string SearchReq { get; set; }
        public string SpecialRemark { get; set; }
        public string TravelType { get; set; }
        public string CouponCode { get; set; }
        public double? Discount { get; set; }
        public double? FinalTotalPrice { get; set; }
        public string SupplierCurrency { get; set; }
        public string TransferTypeName { get; set; }
        public double? DueatAtPickUpAmount { get; set; }
        public DateTime? Created_On { get; set; }
    }
}
