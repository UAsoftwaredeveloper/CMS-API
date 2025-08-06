using System;

namespace Cms.Services.Filters.ActivityAdmin
{
    public class ActivityBookingDetailsFilter:CommonFilter
    {
        public int? BookingID { get; set; }
        public int? AffiliateID { get; set; }
        public int? SupplierID { get; set; }
        public string BookingStatus { get; set; }
        public string SearchKey { get; set; }
        public string SearchReq { get; set; }
        public string ActivityType { get; set; }
        public string CategoryName { get; set; }
        public DateTime? ActivityDt { get; set; }
        public string ActivityDate { get; set; }
        public string BillerEmailId { get; set; }
        public string BillerPhone { get; set; }
        public string Destination { get; set; }
        public string Currency { get; set; }
        public long? Created_By { get; set; }
        public bool? SortAscending { get; set; }
        public bool? SortDescending { get; set; }
        public bool? UpComing { get; set; }
        public bool? Completed { get; set; }
        public bool? Cancelled { get; set; }
    }
}
