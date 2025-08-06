using System;

namespace Cms.Services.Models.TMMModals
{
    public class EnqueryPageDetailsModal
    {
        public int Id { get; set; }
        public int? PageId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Page_Name { get; set; } = string.Empty;
        public string PageUrl { get; set; } = string.Empty;
        public string EnqueryDetails { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerIp { get; set; } = string.Empty;
        public string PageType { get; set; } = string.Empty;
        public string PackagePrice { get; set; } = string.Empty;
        public string PackageCurrency { get; set; } = string.Empty;
        public string PackageRefId { get; set; } = string.Empty;
        public string AirportName { get; set; } = string.Empty;
        public bool? Deleted { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; } =0;
    }
}
