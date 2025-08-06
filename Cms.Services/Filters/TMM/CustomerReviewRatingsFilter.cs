using System;

namespace Cms.Services.Filters.TMM
{
    public class CustomerReviewRatingsFilter:CommonFilter
    {
        public string PageType { get; set; }
        public string FileType { get; set; }
        public int? UserId { get; set; }
        public int? PortalId { get; set; }
        public bool? Approved { get; set; }
        public string emailId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Ratings { get; set; }
        public DateTime ApprovedDate { get; set; }
        public bool? SortAscending { get; set; }
        public bool? SortDescending { get; set; }
        public bool? SortHighestRating { get; set; }
        public bool? SortLowestRating { get; set; }

    }
}
