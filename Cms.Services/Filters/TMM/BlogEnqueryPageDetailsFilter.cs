using System;

namespace Cms.Services.Filters.TMM
{
    public class BlogEnqueryPageDetailsFilter:CommonFilter
    {
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string Page_Name { get; set; }
        public string PageUrl { get; set; }
        public int PageId { get; set; }
        public string PageType { get; set; }
        public string DeviceType { get; set; }
        public string DestinationName { get; set; }
        public string TravellerCount { get; set; }
        public string TravelDate { get; set; }
        public string Duration { get; set; }
        public string EnquiryRefId { get; set; }


    }
}
