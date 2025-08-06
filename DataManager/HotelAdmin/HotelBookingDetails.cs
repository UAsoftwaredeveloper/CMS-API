using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.HotelAdmin
{
    [Table("Hotel_BookingDetails")]
    public class HotelBookingDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingID { get; set; }
        public string SearchKey { get; set; }
        public string RepriceKey { get; set; }
        public int? AffiliateID { get; set; }
        public int? SupplierID { get; set; }
        public string HotelCode { get; set; }
        public string Destination { get; set; }
        public string HotelName { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public int? RoomCount { get; set; }
        public int? AdultCount { get; set; }
        public int? ChildCount { get; set; }
        public string BookedOn { get; set; }
        public double? TotalPrice { get; set; }
        public bool? IsRefundable { get; set; }
        public double? Markup { get; set; }
        public double? SupplierBaseFare { get; set; }
        public string BookingStatus { get; set; }
        public string Currency { get; set; }
        public string IPAddress { get; set; }
        public string BillerEmailId { get; set; }
        public string BillerPhone { get; set; }
        public string BillerName { get; set; }
        public string CountryCode { get; set; }
        public string MealType { get; set; }
        public string SearchReq { get; set; }
        public DateTime? Created_On { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public string SpecialRemark { get; set; }
        public string HotelAddress { get; set; }
        public string StarRating { get; set; }
        public string Address { get; set; }
        public long? CreatedBy { get; set; }

        public double? TotalFinalPrice { get; set; }
        public double? Discount { get; set; }
        public string CouponCode { get; set; }
        public string BookingRef { get; set; }
    }
}
