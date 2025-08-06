namespace Cms.Services.Filters.TMM
{
    public class CruiseBookingTransactionDetailsFilter:CommonFilter
    {
        public long? TransactionId { get; set; }
        public string SearchGuid { get; set; }
        public string PNRNo { get; set; }
        public bool? IsBooked { get; set; }
        public int? PortalID { get; set; }
        public string CruiseTitle { get; set; }
        public string Duration { get; set; }
        public string CruiseLineName { get; set; }
        public string ShipName { get; set; }
        public double? TotalPrice { get; set; }
        public string CouponCode { get; set; }
        public string ToCurrencyType { get; set; }
        public string DeviceType { get; set; }
        public string BillerCity { get; set; }
        public string BillerEmailId { get; set; }
        public string BillerPhone { get; set; }
        public string CardType { get; set; }
        public string CustRefNo { get; set; }
        public string BookingMode { get; set; }
        public  long? Created_By { get; set; }
        public bool? SortAscending { get; set; }
        public bool? SortDescending { get; set; }
        public bool? UpComing { get; set; }
        public bool? Completed { get; set; }
        public bool? Cancelled { get; set; }
    }
}