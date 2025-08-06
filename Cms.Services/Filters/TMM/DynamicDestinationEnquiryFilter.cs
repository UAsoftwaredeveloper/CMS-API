namespace Cms.Services.Filters.TMM
{
    public class DynamicDestinationEnquiryFilter:CommonFilter
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? PortalId { get; set; }
        public string RefrenceId { get; set; }
    }
}
