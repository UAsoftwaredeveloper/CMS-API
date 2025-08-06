using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.ActivityAdmin
{
    [Table("Activity_BookingDetails")]
    public class ActivityBookingDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? BookingID { get; set; }
        public int? AffiliateID { get; set; }
        public int? SupplierID { get; set; }
        public string BookingStatus { get; set; }
        public string SearchKey { get; set; }
        public string RepriceKey { get; set; }
        public string SearchReq { get; set; }
        public string ActivityType { get; set; }
        public string CategoryName { get; set; }
        public DateTime? ActivityDt { get; set; }
        public string ActivityDate { get; set; }
        public string BillerEmailId { get; set; }
        public string BillerPhone { get; set; }
        public string Addres { get; set; }
        public string zipCode { get; set; }
        public string Destination { get; set; }
        public string CountryCode { get; set; }
        public string AdultCount { get; set; }
        public string ChildCount { get; set; }
        public string BillerName { get; set; }
        public string Currency { get; set; }
        public string Markup { get; set; }
        public double? SupplierBaseFare { get; set; }
        public double? TotalPrice { get; set; }
        public string IPAddress { get; set; }
        public string SpecialRemark { get; set; }
        public string BookedOn { get; set; }
        public string ActivityImage { get; set; }
        public DateTime Created_On { get; set; }
        public long? CreatedBy { get; set; }
        public double? TotalFinalPrice { get; set; }
        public double? Discount { get; set; }
        public string CouponCode { get; set; }
    }
}
