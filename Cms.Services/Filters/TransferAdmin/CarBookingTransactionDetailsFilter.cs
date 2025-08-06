namespace Cms.Services.Filters.TransferAdmin
{

    public class CarBookingTransactionDetailsFilter:CommonFilter
    {
        public string SearchKey { get; set; }
        public string RepriceKey { get; set; }
        public string TripType { get; set; }
        public string BookingStatus { get; set; }
        public string BillerEmailId { get; set; }
        public string SearchReq { get; set; }
        public string TravelType { get; set; }
        public string TransferTypeName { get; set; }
        public int? PortalID { get; set; }
        public int? AffiliateID { get; set; }
        public int? SupplierID { get; set; }
        public long? Created_By { get; set; }

    }
}
