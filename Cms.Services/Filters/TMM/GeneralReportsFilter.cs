namespace Cms.Services.Filters.TMM
{
    public class GeneralReportsFilter:CommonFilter
    {
        public int? PortalId { get; set; }
        public string ReferenceNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
    }
}
