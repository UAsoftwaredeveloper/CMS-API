using System;

namespace Cms.Services.Filters
{
    public class HolidayPackagesFilter:CommonFilter
    {
        public string PortalIds { get; set; }
        public string Url { get; set; }
        public string Keywords { get; set; }
        public string Title { get; set; }
        public string PackageName { get; set; }
        public int Number_of_Nights { get; set; }
        public int Number_of_Days { get; set; }
        public string OriginCityName { get; set; }
        public string DestinationCityName { get; set; }
        public string LocationTitle { get; set; }
        public string Theme {  get; set; }
        public int MinPrimaryThemeQuantity { get; set; } = 4;
        public decimal? Amount { get; set; }
        public decimal? AmountFrom { get; set; }
        public decimal? AmountTo { get; set; }
        public int StarRatings { get; set; }
        public string CountryName { get; set; }
        public bool? ShowOnHomePage { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public int SimilarItems { get; set; } = 10;
        public bool IncludeSimilarItems {  get; set; }=false;
        public bool? Approved { get; set; }
    }
}
