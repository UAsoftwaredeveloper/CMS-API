namespace Cms.Services.Filters.HotelAdmin
{
    public class HotelBookingDetailsFilter : CommonFilter
    {
        public int? BookingID { get; set; }
        public string BillerEmailId { get; set; }
        public string SearchKey { get; set; }
        public string RepriceKey { get; set; }
        public int? AffiliateID { get; set; }
        public int? SupplierID { get; set; }
        public string HotelCode { get; set; }
        public string Destination { get; set; }
        public string HotelName { get; set; }
        public string BookedOn { get; set; }
        public double? TotalPrice { get; set; }
        public double? Markup { get; set; }
        public string BookingStatus { get; set; }
        public string SearchReq { get; set; }
        public long? Created_By { get; set; }
        public bool? SortAscending { get; set; }
        public bool? SortDescending { get; set; }
        public bool? UpComing { get; set; }
        public bool? Completed { get; set; }
        public bool? Cancelled { get; set; }
    }
}
