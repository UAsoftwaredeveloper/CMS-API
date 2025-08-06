using System;

namespace Cms.Services.Models.TMMModals
{
    public class BlogEnqueryPageDetailsModal
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string EnqueryDetails { get; set; }
        public string Page_Name { get; set; }
        public string PageUrl { get; set; }
        public int PageId { get; set; }
        public string PageType { get; set; }
        public string CustomerIp { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public bool Deleted { get; set; }
        public string DeviceType { get; set; }
        public string DestinationName { get; set; }
        public string TravellerCount { get; set; }
        public string TravelDate { get; set; }
        public string Duration { get; set; }
        public string EnquiryRefId { get; set; }


    }
}
