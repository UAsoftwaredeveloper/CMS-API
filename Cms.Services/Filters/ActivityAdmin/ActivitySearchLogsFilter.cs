namespace Cms.Services.Filters.ActivityAdmin
{
    public class ActivitySearchLogsFilter:CommonFilter
    {
        public int? AffiliateId { get; set; }
        public string Destination { get; set; }
        public string CityCode { get; set; }
        public string HotelCode { get; set; }
        public string CountryCode { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int? TotalPax { get; set; }
        public string Nationality { get; set; }
        public string Currency { get; set; }
        public string cultureID { get; set; }
        public string deviceType { get; set; }


    }
}
