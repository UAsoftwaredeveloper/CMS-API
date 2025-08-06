using System;

namespace Cms.Services.Filters
{
    public class CouponMasterFilter : CommonFilter
    {
        public int? PortalId { get; set; }
        public string CouponCode { get; set; }
        public string CouponType { get; set; }
        public string ServiceCategory {  get; set; }
        public bool? IsSpecial {  get; set; }
        public bool? IsMultiUse {  get; set; }
        public string Status {  get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? ShowOnHomePage { get; set; }
    }
}
