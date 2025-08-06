namespace Cms.Services.Filters.TMM
{
    public class BookingTransactionDetailsFilter:CommonFilter
    {
        public int? PortalID { get; set; }
        public string PNRNo { get; set; }
        public string SearchGuid { get; set; }
        public bool? IsBooked { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string CabinClass { get; set; }
        public string SupplierName { get; set; }
        public string FareType { get; set; }
        public string DeviceType { get; set; }
        public string UTMSource { get; set; }
        public string BillerName { get; set; }
        public string BillerPhone { get; set; }
        public string BillerEmailId { get; set; }
        public string CouponCode { get; set; }
        public string CustRefNo { get; set; }
        public long? Created_By { get; set; }

    }
}
