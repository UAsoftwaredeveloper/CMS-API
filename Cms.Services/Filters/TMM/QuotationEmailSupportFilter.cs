namespace Cms.Services.Filters.TMM
{
    public class QuotationEmailSupportFilter:CommonFilter
    {
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string Origine { get; set; }
        public string Destination { get; set; }
        public string CustomerIp { get; set; }
        public string DeviceType { get; set; }
        public int? PortalId { get; set; }
        public bool? SortAscending { get; set; }
        public bool? SortDescending { get; set; }
        public bool? SortHighestRating { get; set; }
        public bool? SortLowestRating { get; set; }
    }
}
