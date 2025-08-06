using System;

namespace Cms.Services.Models.ActivityAdmin
{
    public class ActivitySearchLogsModal
    {
        public int Id { get; set; }
        public string SearchId { get; set; }
        public string SearchKey { get; set; }
        public int AffiliateId { get; set; }
        public string Destination { get; set; }
        public string CityCode { get; set; }
        public string HotelCode { get; set; }
        public string CountryCode { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int TotalPax { get; set; }
        public string GeoLocation { get; set; }
        public string Nationality { get; set; }
        public string Currency { get; set; }
        public string cultureID { get; set; }
        public string deviceType { get; set; }
        public DateTime CreatedOn { get; set; }


    }
}
