namespace Cms.Services.Filters.FrontEnd
{
    public class FlightBookingTransactionFilter : CommonFilter
    {
        public int? PortalID { get; set; }
        public string PNRNo { get; set; }
        public string SearchGuid { get; set; }
        public bool? IsBooked { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public bool? UpComing { get; set; }
        public bool? Completed { get; set; }
        public bool? Cancelled { get; set; }
        public string BillerEmailId { get; set; }
        public string AirlineName { get; set; }
        public long? Created_By { get; set; }
        public bool? SortAscending { get; set; }
        public bool? SortDescending { get; set; }
    }

}
